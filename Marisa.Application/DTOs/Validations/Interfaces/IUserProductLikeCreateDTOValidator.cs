namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IUserProductLikeCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserProductLikeDTO userProductLikeDTO);
    }
}
