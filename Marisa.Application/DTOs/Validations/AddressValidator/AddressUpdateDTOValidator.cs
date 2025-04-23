using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.AddressValidator
{
    public class AddressUpdateDTOValidator : AbstractValidator<AddressDTO>, IAddressUpdateDTOValidator
    {
        public AddressUpdateDTOValidator() 
        {
            RuleFor(x => x.Id.ToString())
                .NotEmpty().WithMessage("addressId must not be empty.")
                .NotNull().WithMessage("addressId must not be null.")
                .Matches(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[1-5][0-9a-fA-F]{3}\-[89abAB][0-9a-fA-F]{3}\-[0-9a-fA-F]{12}$")
                .WithMessage("addressId must be a valid UUID.");

            RuleFor(x => x.AddressNickname)
                    .NotEmpty().WithMessage("AddressNickname must not be empty.")
                    .NotNull().WithMessage("AddressNickname must not be null.")
                    .MinimumLength(3).WithMessage("AddressNickname must be at least 3 characters long");

            RuleFor(x => x.AddressType)
                .NotEmpty().WithMessage("AddressType must not be empty.")
                .NotNull().WithMessage("AddressType must not be null.")
                .MinimumLength(3).WithMessage("AddressType must be at least 3 characters long");

            RuleFor(x => x.RecipientName)
                .NotEmpty().WithMessage("RecipientName must not be empty.")
                .NotNull().WithMessage("RecipientName must not be null.")
                .MinimumLength(3).WithMessage("RecipientName must be at least 3 characters long");

            RuleFor(x => x.ZipCode)
               .NotEmpty().WithMessage("ZipCode must not be empty.")
               .NotNull().WithMessage("ZipCode must not be null.")
               .MinimumLength(8).WithMessage("ZipCode must be at least 8 characters long");

            RuleFor(x => x.Street)
               .NotEmpty().WithMessage("Street must not be empty.")
               .NotNull().WithMessage("Street must not be null.")
               .MinimumLength(3).WithMessage("Street must be at least 8 characters long");

            RuleFor(x => x.Number)
               .NotNull().WithMessage("Number must not be null.")
               .GreaterThanOrEqualTo(1).WithMessage("Number must be greater than zero.");

            //RuleFor(x => x.Complement)
            //   .NotEmpty().WithMessage("Complement must not be empty.")
            //   .NotNull().WithMessage("Complement must not be null.")
            //   .MinimumLength(3).WithMessage("Complement must be at least 8 characters long");

            RuleFor(x => x.Neighborhood)
               .NotEmpty().WithMessage("Neighborhood must not be empty.")
               .NotNull().WithMessage("Neighborhood must not be null.")
               .MinimumLength(3).WithMessage("Neighborhood must be at least 8 characters long");

            RuleFor(x => x.City)
               .NotEmpty().WithMessage("City must not be empty.")
               .NotNull().WithMessage("City must not be null.")
               .MinimumLength(3).WithMessage("City must be at least 8 characters long");

            RuleFor(x => x.Neighborhood)
               .NotEmpty().WithMessage("State must not be empty.")
               .NotNull().WithMessage("State must not be null.")
               .MinimumLength(3).WithMessage("State must be at least 8 characters long");

            //RuleFor(x => x.ReferencePoint)
            //   .NotEmpty().WithMessage("ReferencePoint must not be empty.")
            //   .NotNull().WithMessage("ReferencePoint must not be null.")
            //   .MinimumLength(3).WithMessage("ReferencePoint must be at least 8 characters long");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId must not be empty.")
                .NotNull().WithMessage("UserId must not be null.")
                .Matches(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[1-5][0-9a-fA-F]{3}\-[89abAB][0-9a-fA-F]{3}\-[0-9a-fA-F]{12}$")
                .WithMessage("UserId must be a valid UUID.");
        }

        public ValidationResult ValidateDTO(AddressDTO addressDTO)
        {
            return Validate(addressDTO);
        }
    }
}
