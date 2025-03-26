using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserValidator
{
    public class UserUpdateProfileDTOValidator : AbstractValidator<UserDTO>, IUserUpdateProfileDTOValidator
    {
        public UserUpdateProfileDTOValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .NotNull().WithMessage("Name must not be null.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Token must not be empty.")
                .NotNull().WithMessage("Token must not be null.");

            RuleFor(x => x.CellPhone)
                .NotEmpty().WithMessage("Token must not be empty.")
                .NotNull().WithMessage("Token must not be null.")
                .Matches(@"^\d{2} \d{5}-\d{4}$")
                .WithMessage("matches invalid CellPhone");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Token must not be empty.")
                .NotNull().WithMessage("Token must not be null.")
                .Matches(@"^\d{2} \d{4}-\d{4}$")
                .WithMessage("matches invalid CellPhone");

            RuleFor(x => x.BirthDateString)
                .NotEmpty().WithMessage("Token must not be empty.")
                .NotNull().WithMessage("Token must not be null.")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("matches invalid BirthDateString");
        }

        public ValidationResult ValidateDTO(UserDTO userDTO)
        {
            return Validate(userDTO);
        }
    }
}
