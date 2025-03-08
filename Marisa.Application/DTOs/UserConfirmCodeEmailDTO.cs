namespace Marisa.Application.DTOs
{
    public class UserConfirmCodeEmailDTO
    {
        public string? Code { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public bool CorrectCode { get; set; } = false;

        public UserConfirmCodeEmailDTO(string? code, string? userId, string? email, bool correctCode)
        {
            Code = code;
            UserId = userId;
            Email = email;
            CorrectCode = correctCode;
        }

        public UserConfirmCodeEmailDTO()
        {
        }
    }
}
