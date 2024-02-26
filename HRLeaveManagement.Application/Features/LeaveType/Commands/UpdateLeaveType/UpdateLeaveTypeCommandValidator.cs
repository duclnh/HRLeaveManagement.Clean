using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Id)
                    .NotNull()
                    .MustAsync(LeaveTypeMustExist)
                    .WithMessage("{LeaveType is not exist}");


            RuleFor(p => p.Name)
                  .NotEmpty().WithMessage("{PropertyName} is required")
                  .NotNull()
                  .MaximumLength(70).WithMessage("{PropertyName must be fewer than 70 characters");

            RuleFor(p => p.DefaultDays)
                 .LessThan(100).WithMessage("{PropertyDefaultDays} cannot exceed 100")
                 .GreaterThan(1).WithMessage("{PropertyDefaultDays} cannot less than 1");

            RuleFor(q => q)
               .MustAsync(LeaveTypeNameUnique)
               .WithMessage("Leave type already exists");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeMustExist(int arg1, CancellationToken token)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(arg1);

            return leaveType != null;
        }

        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }

    }
}
