using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDTO>
    {
        public int Id { get; set; }
    }
}
