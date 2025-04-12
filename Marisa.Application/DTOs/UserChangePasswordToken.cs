namespace Marisa.Application.DTOs
{
    public class UserChangePasswordToken
    {
        public string? UserId { get; set; }
        public string? NewPassword { get; set; }

        public UserChangePasswordToken(string? userId, string? newPassword)
        {
            UserId = userId;
            NewPassword = newPassword;
        }

        public void SetUserId(string? userId)
        {
            UserId = userId;
        }

        public void SetNewPassword(string? newPassword)
        {
            NewPassword = newPassword;
        }
    }
}
