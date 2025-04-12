using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserValidator
{
    public class ChangePasswordUserDTOValidator : AbstractValidator<ChangePasswordUser>, IChangePasswordUserDTOValidator
    {
        public ChangePasswordUserDTOValidator() 
        {
            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId must not be empty.")
                    .NotNull().WithMessage("UserId must not be null.")
                    .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.CurrentPassword)
                   .NotEmpty().WithMessage("CurrentPassword must not be empty.")
                   .NotNull().WithMessage("CurrentPassword must not be null.")
                   .MinimumLength(6).WithMessage("CurrentPassword must be at least 6 characters long.");

            RuleFor(x => x.ConfirmNewPassword)
                   .NotEmpty().WithMessage("ConfirmNewPassword must not be empty.")
                   .NotNull().WithMessage("ConfirmNewPassword must not be null.")
                   .MinimumLength(6).WithMessage("ConfirmNewPassword must be at least 6 characters long.");
        }

        private bool BeAValidGuid(string? userId)
        {
            return Guid.TryParse(userId, out _);
        }

        public ValidationResult ValidateDTO(ChangePasswordUser userChangePasswordToken)
        {
            return Validate(userChangePasswordToken);
        }
    }
}
