namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IAddressCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(AddressDTO addressDTO);
    }
}
