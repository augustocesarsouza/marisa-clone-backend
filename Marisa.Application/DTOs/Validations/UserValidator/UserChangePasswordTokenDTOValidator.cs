using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserValidator
{
    public class UserChangePasswordTokenDTOValidator : AbstractValidator<UserChangePasswordToken>, IUserChangePasswordTokenDTOValidator
    {
        public UserChangePasswordTokenDTOValidator()
        {
            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId must not be empty.")
                    .NotNull().WithMessage("UserId must not be null.")
                    .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.NewPassword)
                   .NotEmpty().WithMessage("NewPassword must not be empty.")
                   .NotNull().WithMessage("NewPassword must not be null.")
                   .MinimumLength(6).WithMessage("NewPassword must be at least 6 characters long.");
        }
        private bool BeAValidGuid(string? userId)
        {
            return Guid.TryParse(userId, out _);
        }

        public ValidationResult ValidateDTO(UserChangePasswordToken userChangePasswordToken)
        {
            return Validate(userChangePasswordToken);
        }
    }
}
