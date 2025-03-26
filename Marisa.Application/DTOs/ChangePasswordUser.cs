namespace Marisa.Application.DTOs
{
    public class ChangePasswordUser
    {
        public string? UserId { get; set; }
        public string? CurrentPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }

        public ChangePasswordUser()
        {
        }

        public ChangePasswordUser(string? userId, string? currentPassword, string? confirmNewPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }
}
