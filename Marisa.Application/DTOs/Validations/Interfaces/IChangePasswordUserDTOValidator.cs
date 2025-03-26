namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IChangePasswordUserDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ChangePasswordUser changePasswordUser);
    }
}
