namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IUserCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserDTO userDTO);
    }
}
