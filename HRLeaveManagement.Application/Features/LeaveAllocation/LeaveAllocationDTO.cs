using HRLeaveManagement.Application.Features.LeaveType.Queries;

namespace HRLeaveManagement.Application.Features.LeaveAllocation
{
    public class LeaveAllocationDTO
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDTO? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
