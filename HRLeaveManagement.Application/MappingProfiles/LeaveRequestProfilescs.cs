using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfiles : Profile
    {
        public LeaveRequestProfiles()
        {
            CreateMap<LeaveRequestDTO, LeaveRequest>().ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
