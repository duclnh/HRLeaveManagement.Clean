using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _dbContext.LeaveTypes.AnyAsync(l => l.Name == name) == false;
        }
    }

}
