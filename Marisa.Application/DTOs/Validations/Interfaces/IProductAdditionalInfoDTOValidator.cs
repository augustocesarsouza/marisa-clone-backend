namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IProductAdditionalInfoDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductAdditionalInfoDTO productAdditionalInfoDTO);
    }
}