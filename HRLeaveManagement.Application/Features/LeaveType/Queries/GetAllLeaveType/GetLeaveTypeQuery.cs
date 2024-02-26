using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType
{
    public record GetLeaveTypeQuery() : IRequest<List<LeaveTypeDTO>>;
}
