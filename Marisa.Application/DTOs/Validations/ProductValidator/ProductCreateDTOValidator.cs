using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.ProductValidator
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductDTO>, IProductCreateDTOValidator
    {
        public ProductCreateDTOValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title must not be empty.")
                .NotNull().WithMessage("Title must not be null.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price must not be empty.")
                .NotNull().WithMessage("Price must not be null.")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.DiscountPercentage)
                .NotEmpty().WithMessage("DiscountPercentage must not be empty.")
                .NotNull().WithMessage("DiscountPercentage must not be null.")
                .GreaterThan(0).WithMessage("DiscountPercentage must be greater than 0");

            RuleFor(x => x.InstallmentTimesMarisaCard)
                .NotEmpty().WithMessage("InstallmentTimesMarisaCard must not be empty.")
                .NotNull().WithMessage("InstallmentTimesMarisaCard must not be null.")
                .GreaterThan(0).WithMessage("InstallmentTimesMarisaCard must be greater than 0");

            RuleFor(x => x.InstallmentTimesCreditCard)
                .NotEmpty().WithMessage("InstallmentTimesCreditCard must not be empty.")
                .NotNull().WithMessage("InstallmentTimesCreditCard must not be null.")
                .GreaterThan(0).WithMessage("InstallmentTimesCreditCard must be greater than 0");

            RuleFor(x => x.Colors)
                .NotNull().WithMessage("Colors must not be null.")
                .Must(colors => colors!.Any()).WithMessage("Colors must contain at least one item.");

            RuleFor(x => x.SizesAvailable)
                .NotNull().WithMessage("SizesAvailable must not be null.")
                .Must(colors => colors!.Any()).WithMessage("SizesAvailable must contain at least one item.");

            RuleFor(x => x.ImageUrlBase64)
                .NotEmpty().WithMessage("ImageUrlBase64 must not be empty.")
                .NotNull().WithMessage("ImageUrlBase64 must not be null.")
                .MinimumLength(20).WithMessage("ImageUrlBase64 must be at least 3 characters long");

            RuleFor(x => x.QuantityAvailable)
                .NotEmpty().WithMessage("QuantityAvailable must not be empty.")
                .NotNull().WithMessage("QuantityAvailable must not be null.")
                .GreaterThan(0).WithMessage("QuantityAvailable must be greater than 0");
        }
        

        public ValidationResult ValidateDTO(ProductDTO productDTO)
        {
            return Validate(productDTO);
        }
    }
}
