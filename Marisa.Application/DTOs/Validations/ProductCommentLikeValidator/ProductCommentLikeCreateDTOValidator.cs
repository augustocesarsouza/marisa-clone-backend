
using FluentValidation;
using FluentValidation.Results;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Domain.Enums;

namespace Marisa.Application.DTOs.Validations.ProductCommentLikeValidator
{
    public class ProductCommentLikeCreateDTOValidator : AbstractValidator<ProductCommentLikeCreate>, IProductCommentLikeCreateDTOValidator
    {
        public ProductCommentLikeCreateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId must not be null.")
                .NotEmpty().WithMessage("UserId must not be empty.")
                .Must(BeValidGuid).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.ProductCommentId)
                .NotNull().WithMessage("ProductCommentId must not be null.")
                .NotEmpty().WithMessage("ProductCommentId must not be empty.")
                .Must(BeValidGuid).WithMessage("ProductCommentId must be a valid GUID.");

            RuleFor(x => x.ProductId)
               .NotNull().WithMessage("ProductId must not be null.")
               .NotEmpty().WithMessage("ProductId must not be empty.")
               .Must(BeValidGuid).WithMessage("ProductId must be a valid GUID.");

            RuleFor(x => x.Reaction)
            .IsInEnum().WithMessage("Reaction must be a valid reaction (Like or Dislike).");
        }

        private bool BeValidGuid(string? guidString)
        {
            return Guid.TryParse(guidString, out _);
        }

        public ValidationResult ValidateDTO(ProductCommentLikeCreate productCommentLikeCreate)
        {
            return Validate(productCommentLikeCreate);
        }
    }
}
