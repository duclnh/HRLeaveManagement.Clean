using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations
                        .AnyAsync(q => q.EmployeeId == userId 
                              && q.LeaveTypeId == leaveTypeId
                              && q.Period == period
                        );
        }

        public async Task<LeaveAllocation?> GetLeaveAllocationWithDeatails(int id)
        {
            return await _dbContext.LeaveAllocations
                 .Include(q => q.LeaveType)
                 .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            return await _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)    
                .ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            return await _dbContext.LeaveAllocations
                    .Where(q => q.EmployeeId == userId)
                    .Include(q => q.LeaveType)
                    .ToListAsync();
        }

        public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations
                 .Include(q => q.LeaveType)
                 .FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
        }
    }

}
