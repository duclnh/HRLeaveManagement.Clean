using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.Configurations
{
    public class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
            builder.HasOne<LeaveType>(la => la.LeaveType)
                   .WithMany(lt => lt.leaveAllocations)
                   .HasForeignKey(la => la.LeaveTypeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
