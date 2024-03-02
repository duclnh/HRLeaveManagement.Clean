using AutoMapper;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

         public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, 
                                                   IMapper mapper, 
                                                   IAppLogger<GetAllLeaveAllocationQueryHandler> logger,
                                                   ILeaveTypeRepository leaveTypeRepository,
                                                   IUserService userService)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Allocation Create Request", validationResult);
            
            //Get leave type for allocations
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            if (leaveType == null)
                throw new NotFoundException($"Leave type {request.LeaveTypeId} not found", request.LeaveTypeId);

            //get employees
            var employees = await _userService.GetEmployees();

            //Get period
            var period = DateTime.Now.Year;

            //Asgin allocations IF an allocation doesn't already exist for period and leave type
            var allocations = new List<Domain.LeaveAllocation>();
            foreach(var employee in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(employee.Id, leaveType.Id ,period);
                if (!allocationExists)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }
            }

            if (allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);
            }

            return Unit.Value;
        }
    }
}
