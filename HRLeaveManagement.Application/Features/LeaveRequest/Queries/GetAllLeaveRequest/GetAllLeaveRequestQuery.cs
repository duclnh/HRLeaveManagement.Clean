using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest
{
    public class GetAllLeaveRequestQuery : IRequest<List<LeaveRequestDTO>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
