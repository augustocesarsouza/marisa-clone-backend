namespace Marisa.Application.DTOs
{
    public class UserTokenSentEmail
    {
        public bool TokenSentEmail { get; set; }
        public string Message { get; set; } = string.Empty;

        public UserTokenSentEmail(bool tokenSentEmail, string message)
        {
            TokenSentEmail = tokenSentEmail;
            Message = message;
        }

        public UserTokenSentEmail() { }

        public bool GetTokenSentEmail() => TokenSentEmail;
        public string GetMessage() => Message;

        public void SetTokenSentEmail(bool tokenSentEmail)
        {
            TokenSentEmail = tokenSentEmail;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }
    }
}
