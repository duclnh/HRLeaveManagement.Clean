using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation
{
    public class GetAllLeaveAllocationQueryHandler : IRequestHandler<GetAllLeaveAllocationQuery, List<LeaveAllocationDTO>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

        public GetAllLeaveAllocationQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IAppLogger<GetAllLeaveAllocationQueryHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LeaveAllocationDTO>> Handle(GetAllLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
       
            var data = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);

            _logger.LogInformation("Leave allocation retrieved data");

            return data;
        }
    }
}
