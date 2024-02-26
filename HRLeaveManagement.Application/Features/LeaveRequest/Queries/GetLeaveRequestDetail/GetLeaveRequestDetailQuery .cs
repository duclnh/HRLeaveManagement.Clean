using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDTO>
    {
        public int Id { get; set; }
    }
}
