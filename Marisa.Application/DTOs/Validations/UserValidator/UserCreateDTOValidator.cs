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
                .NotEmpty().WithMessage("Name must not be empty.")
                .NotNull().WithMessage("Name must not be null.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must not be empty.")
                .NotNull().WithMessage("Email must not be null.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@gmail\.com$")
                .WithMessage("matches invalid Email");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Cpf must not be empty.")
                .NotNull().WithMessage("Cpf must not be null.")
                .Matches(@"^\d{11}$")
                .WithMessage("matches invalid Cpf");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender must not be empty.")
                .NotNull().WithMessage("Gender must not be null.");

            RuleFor(x => x.CellPhone)
                .NotEmpty().WithMessage("CellPhone must not be empty.")
                .NotNull().WithMessage("CellPhone must not be null.")
                .Matches(@"^\d{2} \d{5}-\d{4}$")
                .WithMessage("matches invalid CellPhone");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Telephone must not be empty.")
                .NotNull().WithMessage("Telephone must not be null.")
                .Matches(@"^\d{2} \d{4}-\d{4}$")
                .WithMessage("matches invalid Telephone");

            RuleFor(x => x.BirthDateString)
                .NotEmpty().WithMessage("BirthDateString must not be empty.")
                .NotNull().WithMessage("BirthDateString must not be null.")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("matches invalid BirthDateString");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty.")
                .NotNull().WithMessage("Password must not be null.")
                .Length(8, 30).WithMessage("Password must be between 8 and 30 length");

            RuleFor(x => x.TokenForCreation)
                .NotNull().WithMessage("O token não pode ser nulo.")
                .InclusiveBetween(100000, 999999).WithMessage("O token deve ser um número de 6 dígitos.");
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