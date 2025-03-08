using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserValidator
{
    public class UserSendCodeEmailDTOValidator : AbstractValidator<UserDTO>, IUserSendCodeEmailDTOValidator
    {
        public UserSendCodeEmailDTOValidator()
        {
            //RuleFor(x => x.Name)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Must be informed Name");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .Length(5, 50)
                .Matches(@"@")
                .WithMessage("The Email must contain '@'.");
        }

        public ValidationResult ValidateDTO(UserDTO userDTO)
        {
            return Validate(userDTO);
        }
    }
}
