using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUserService _userService;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender,
                                                IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
                                                ILeaveRequestRepository leaveRequestRepository,
                                                IUserService userService,
                                                ILeaveAllocationRepository leaveAllocationRepository)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _userService = userService;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            // Get requesting employee's id
            if (_userService.UserId == null)
                throw new BadRequestException("Employee invalid");

            var employee = await _userService.GetEmployee(_userService.UserId);

            if (employee == null)
                throw new NotFoundException("Employee not found", _userService.UserId);

            // Check on employee's allocation
            var allocation = await _leaveAllocationRepository.GetUserAllocations(_userService.UserId, request.LeaveTypeId);

            // if allocations aren't enough, return validation error with message
            if (allocation == null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.LeaveTypeId),
                    "You do not have any allocations for this leave type")
                );
                throw new BadRequestException("Invalid leave request", validationResult);
            }

            //if not enough days request, return invalid leave request
            int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
            if (daysRequested > allocation.NumberOfDays)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                   nameof(request.LeaveTypeId),
                   "You do not enough days for this request")
               );
                throw new BadRequestException("Invalid leave request", validationResult);
            }

            // Create leave request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employee.Id;
            leaveRequest.DateRequest = DateTime.Now;
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                // send confirmation email
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                            $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"
                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                throw;
            }


            return Unit.Value;
        }
    }
}
