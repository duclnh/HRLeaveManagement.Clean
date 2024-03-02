using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasOne<LeaveType>(lr => lr.LeaveType)
                   .WithMany(lt => lt.leaveRequests)
                   .HasForeignKey(lt => lt.LeaveTypeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
