using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
    public ICollection<LeaveAllocation>? leaveAllocations { get; set; }
    public ICollection<LeaveRequest>? leaveRequests { get; set; }
}
