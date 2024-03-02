using AutoMapper;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest
{
    public class GetAllLeaveRequestQueryHandler : IRequestHandler<GetAllLeaveRequestQuery,List<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetAllLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository,
                                              IMapper mapper,
                                              IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<LeaveRequestDTO>> Handle(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestDTO>();

            if(_userService.UserId == null)
            {
                throw new BadRequestException("User invalid");
            }

            var employee = await _userService.GetEmployee(_userService.UserId);

            if (employee == null)
                throw new NotFoundException("Employee not found", _userService.UserId);

            if (request.IsLoggedInUser)
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails(_userService.UserId);
                requests = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
                foreach (var re in requests)
                {
                    re.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails();
                requests = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
                foreach (var re in requests)
                {
                    re.Employee = await _userService.GetEmployee(re.RequestingEmployeeId);
                }
            }

            return requests;


        }
    }
}
