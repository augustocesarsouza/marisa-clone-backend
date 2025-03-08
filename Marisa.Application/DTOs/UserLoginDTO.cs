namespace Marisa.Application.DTOs
{
    public class UserLoginDTO
    {
        public bool PasswordIsCorrect { get; set; }
        public UserDTO? UserDTO { get; set; }

        public UserLoginDTO(bool passwordIsCorrect, UserDTO userDTO)
        {
            PasswordIsCorrect = passwordIsCorrect;
            UserDTO = userDTO;
        }

        public UserLoginDTO()
        {
        }
    }
}
