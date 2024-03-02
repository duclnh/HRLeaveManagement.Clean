namespace HRLeaveManagement.Application.Features.LeaveType
{
    public class LeaveTypeDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
