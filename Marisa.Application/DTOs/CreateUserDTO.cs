namespace Marisa.Application.DTOs
{
    public class CreateUserDTO
    {
        public bool? TokenIsValid { get; set; }
        public UserDTO? UserDTO { get; set; }

        public CreateUserDTO(bool? tokenIsValid, UserDTO? userDTO)
        {
            TokenIsValid = tokenIsValid;
            UserDTO = userDTO;
        }

        public bool? SetTokenIsValid(bool? tokenIsValid) => TokenIsValid = tokenIsValid;
        public UserDTO? SetUserDTO(UserDTO? userDTO) => UserDTO = userDTO;
    }
}
