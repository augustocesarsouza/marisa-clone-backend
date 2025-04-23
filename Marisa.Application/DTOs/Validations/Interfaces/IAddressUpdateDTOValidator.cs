namespace Marisa.Application.DTOs.Validations.Interfaces
{
    public interface IAddressUpdateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(AddressDTO addressDTO);
    }
}
