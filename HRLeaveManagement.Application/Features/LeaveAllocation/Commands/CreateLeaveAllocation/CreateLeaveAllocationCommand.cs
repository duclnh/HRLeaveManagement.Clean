using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<LeaveAllocationDTO>
    {
        public int LeaveTypeId { get; set; }
    }
}
