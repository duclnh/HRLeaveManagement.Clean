using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation> 
    {
        Task<LeaveAllocation?> GetLeaveAllocationWithDeatails(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation?> GetUserAllocations(string userId,int leaveTypeId);

    }
}
