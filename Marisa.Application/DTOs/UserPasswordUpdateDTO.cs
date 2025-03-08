namespace Marisa.Application.DTOs
{
    public class UserPasswordUpdateDTO
    {
        public bool PasswordUpdateSuccessfully { get; set; } = false;

        public UserPasswordUpdateDTO(bool passwordUpdateSuccessfully)
        {
            PasswordUpdateSuccessfully = passwordUpdateSuccessfully;
        }
    }
}
