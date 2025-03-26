namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IUserUpdateProfileDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserDTO userDTO);
    }
}
