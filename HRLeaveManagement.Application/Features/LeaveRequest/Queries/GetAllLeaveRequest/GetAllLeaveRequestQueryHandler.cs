using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest
{
    public class GetAllLeaveRequestQueryHandler : IRequestHandler<GetAllLeaveRequestQuery,List<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetAllLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveRequestDTO>> Handle(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            // Check if it is logged in employee

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails();
            var data = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);


            // Fill requests with employee information

            return data;


        }
    }
}
