using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.UserProductLikeValidator
{
    public class UserProductLikeCreateDTOValidator : AbstractValidator<UserProductLikeDTO>, IUserProductLikeCreateDTOValidator
    {
        public UserProductLikeCreateDTOValidator()
        {
            RuleFor(x => x.ProductIdString)
                .NotNull().WithMessage("ProductIdString must not be null.")
                .NotEmpty().WithMessage("ProductIdString must not be empty.")
                .Must(BeValidGuid).WithMessage("ProductIdString must be a valid GUID.");

            RuleFor(x => x.UserIdString)
                .NotNull().WithMessage("UserIdString must not be null.")
                .NotEmpty().WithMessage("UserIdString must not be empty.")
                .Must(BeValidGuid).WithMessage("UserIdString must be a valid GUID.");
        }

        private bool BeValidGuid(string? guidString)
        {
            return Guid.TryParse(guidString, out _);
        }

        public ValidationResult ValidateDTO(UserProductLikeDTO userProductLikeDTO)
        {
            return Validate(userProductLikeDTO);
        }
    }
}
