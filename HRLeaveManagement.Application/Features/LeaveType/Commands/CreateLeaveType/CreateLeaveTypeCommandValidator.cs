using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Name)
                 .NotEmpty().WithMessage("{PropertyName} is required")
                 .NotNull()
                 .MaximumLength(70).WithMessage("{PropertyName must be fewer than 70 characters");

            RuleFor(p => p.DefaultDays)
                 .LessThan(100).WithMessage("{PropertyDefaultDays} cannot exceed 100")
                 .GreaterThanOrEqualTo(1).WithMessage("{PropertyDefaultDays} cannot less than 1");

            RuleFor(q => q.Name)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeNameUnique(string name)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(name);
        }
    }
}
