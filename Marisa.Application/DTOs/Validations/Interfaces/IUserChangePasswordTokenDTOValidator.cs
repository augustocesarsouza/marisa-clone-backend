namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IUserChangePasswordTokenDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserChangePasswordToken userChangePasswordToken);
    }
}
