using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfiles : Profile
    {
        public LeaveAllocationProfiles()
        {
            CreateMap<LeaveAllocation, LeaveAllocationDTO>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}
