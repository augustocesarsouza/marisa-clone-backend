using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserDTO>, IUserCreateDTOValidator
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed one Number Name");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .Matches(@"^[a-zA-Z0-9._%+-]+@gmail\.com$")
                .WithMessage("Must be informed one Number Email");

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\d{11}$")
                .WithMessage("Must be informed one Number Cpf");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed one Number Gender");

            RuleFor(x => x.CellPhone)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\d{2} \d{5} \d{4}$")
                .WithMessage("Must be informed one Number CellPhone");

            RuleFor(x => x.Telephone)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\d{2} \d{4} \d{4}$")
                .WithMessage("Must be informed one Number CelTelephonelPhone");

            RuleFor(x => x.BirthDateString)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("Must be informed one Number BirthDateString");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(8, 30)
                .WithMessage("Must be informed Password of the user");
        }

        public ValidationResult ValidateDTO(UserDTO userDTO)
        {
            return Validate(userDTO);
        }
    }
}
//public Guid? Id { get; set; }
//public string? Name { get; set; }
//public string? Email { get; set; }
//public DateTime? BirthDate { get; set; }
//public string? Cpf { get; set; }
//public char? Gender { get; set; }
//public string? CellPhone { get; set; }
//public string? Telephone { get; set; }
//public string? PasswordHash { get; set; }
//public string? Salt { get; set; }
//public string? UserImage { get; set; }
//public string? Token { get; set; }
//public string? Password { get; set; }