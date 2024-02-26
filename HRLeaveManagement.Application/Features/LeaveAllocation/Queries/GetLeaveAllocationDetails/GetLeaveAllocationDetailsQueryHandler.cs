using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDTO>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

        public GetLeaveAllocationDetailsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IAppLogger<GetAllLeaveAllocationQueryHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LeaveAllocationDTO> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDeatails(request.Id);

            if (leaveAllocation == null) {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            var data = _mapper.Map<LeaveAllocationDTO>(leaveAllocation);

            return data;
        }
    }
}
