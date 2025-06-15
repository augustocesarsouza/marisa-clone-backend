using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.ProductAdditionalInfoValidator
{
    public class ProductAdditionalInfoDTOValidator : AbstractValidator<ProductAdditionalInfoDTO>, IProductAdditionalInfoDTOValidator
    {
        public ProductAdditionalInfoDTOValidator()
        {
            RuleFor(x => x.ImgsSecondary)
                .NotNull().WithMessage("ImgsSecondary must not be null.")
                .Must(list => list != null && list.Count >= 1)
                .WithMessage("ImgsSecondary must contain at least one image.");

            RuleFor(x => x.AboutProduct)
                .NotNull().WithMessage("AboutProduct must not be null.")
                .Must(list => list != null && list.Count >= 1)
                .WithMessage("AboutProduct must contain at least one section.");

            RuleFor(x => x.Composition)
                .NotEmpty().WithMessage("Composition must not be empty.")
                .MinimumLength(3).WithMessage("Composition must be at least 3 characters long.");

            //RuleFor(x => x.ShippingInformation)
            //    .NotEmpty().WithMessage("ShippingInformation must not be empty.")
            //    .MinimumLength(3).WithMessage("ShippingInformation must be at least 3 characters long.");
        }

        public ValidationResult ValidateDTO(ProductAdditionalInfoDTO productAdditionalInfoDTO)
        {
            return Validate(productAdditionalInfoDTO);
        }
    }
}
