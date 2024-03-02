using HRLeaveManagement.Application.Features.LeaveType;
using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Features.LeaveRequest
{
    public class LeaveRequestDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Employee? Employee { get; set; }
        public string RequestingEmployeeId { get; set; } = string.Empty;
        public LeaveTypeDTO? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; } = string.Empty;
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}
