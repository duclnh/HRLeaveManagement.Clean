using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, LeaveAllocationDTO>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, 
                                                   IMapper mapper, 
                                                   IAppLogger<GetAllLeaveAllocationQueryHandler> logger,
                                                   ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LeaveAllocationDTO> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Allocation Create Request", validationResult);

            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            var leaveAllocationToCreate = _mapper.Map<Domain.LeaveAllocation>(request);

            await _leaveAllocationRepository.CreateAsync(leaveAllocationToCreate);

            return _mapper.Map<LeaveAllocationDTO>(leaveAllocationToCreate);
        }
    }
}
