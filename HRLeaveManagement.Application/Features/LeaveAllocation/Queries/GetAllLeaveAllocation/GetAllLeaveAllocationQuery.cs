using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation
{
    public class GetAllLeaveAllocationQuery : IRequest<List<LeaveAllocationDTO>>
    {
    }
}
