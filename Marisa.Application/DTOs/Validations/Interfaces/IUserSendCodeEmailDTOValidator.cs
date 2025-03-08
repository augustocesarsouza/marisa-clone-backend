namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IUserSendCodeEmailDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserDTO userDTO);
    }
}
