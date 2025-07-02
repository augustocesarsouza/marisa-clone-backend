namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IProductCommentCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductCommentCreate productCommentCreate);
    }
}
