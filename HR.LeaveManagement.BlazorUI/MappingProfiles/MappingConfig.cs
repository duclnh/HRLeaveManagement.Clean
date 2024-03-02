using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

            CreateMap<LeaveRequestDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.StartDate))
                .ReverseMap();
            CreateMap<LeaveRequestDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.StartDate))
                .ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

            CreateMap<LeaveAllocationDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();

            CreateMap<EmployeeVM, Employee>().ReverseMap();

        }
    }
}
