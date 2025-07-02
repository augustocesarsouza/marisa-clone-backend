using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;

namespace Marisa.Application.DTOs.Validations.ProductCommentValidator
{
    public class ProductCommentCreateDTOValidator : AbstractValidator<ProductCommentCreate>, IProductCommentCreateDTOValidator
    {
        public ProductCommentCreateDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("ProductId must not be null.")
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .Must(BeValidGuid).WithMessage("ProductId must be a valid GUID.");

            RuleFor(x => x.StarQuantity)
               .NotNull().WithMessage("StarQuantity must not be null.");

            RuleFor(x => x.RecommendProduct)
              .NotNull().WithMessage("RecommendProduct must not be null.");

            RuleFor(x => x.Comment)
               .NotEmpty().WithMessage("Comment must not be empty.")
               .NotNull().WithMessage("Comment must not be null.")
               .MinimumLength(3).WithMessage("Comment must be at least 3 characters long");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .NotNull().WithMessage("Name must not be null.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
             
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email must not be empty.")
               .NotNull().WithMessage("Email must not be null.")
               .MinimumLength(3).WithMessage("Email must be at least 3 characters long");

            RuleFor(x => x.ImgProduct)
              .NotNull().WithMessage("ImgProduct must not be null.");
        }

        private bool BeValidGuid(string? guidString)
        {
            return Guid.TryParse(guidString, out _);
        }

        public ValidationResult ValidateDTO(ProductCommentCreate productCommentCreate)
        {
            return Validate(productCommentCreate);
        }
    }
}
