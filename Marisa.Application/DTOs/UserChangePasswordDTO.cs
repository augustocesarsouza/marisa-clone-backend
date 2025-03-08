namespace Marisa.Application.DTOs
{
    public class UserChangePasswordDTO
    {
        //public Guid? Id { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }

        //public string NewPassword { get; set; }

        public UserChangePasswordDTO(string email, string confirmPassword)
        {
            Email = email;
            ConfirmPassword = confirmPassword;
        }
    }
}
