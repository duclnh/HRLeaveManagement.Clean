using AutoMapper;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDTO>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository,
                                                 IMapper mapper,
                                                 IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<LeaveRequestDTO> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {

            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            // Add Employee details as needed
            var data = _mapper.Map<LeaveRequestDTO>(leaveRequest);

            data.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

            return data;
        }
    }
}
