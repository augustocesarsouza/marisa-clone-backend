namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IProductCommentLikeCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductCommentLikeCreate productCommentLikeCreate);
    }
}
