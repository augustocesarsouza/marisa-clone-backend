namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IProductCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductDTO productDTO);
    }
}
