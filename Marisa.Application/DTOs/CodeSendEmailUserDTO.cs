namespace Marisa.Application.DTOs
{
    public class CodeSendEmailUserDTO
    {
        public string? Code { get; set; }
        public bool CodeSendToEmailSuccessfully { get; set; }
        public bool UserAlreadyExist { get; set; }

        public CodeSendEmailUserDTO(string? code, bool codeSendToEmailSuccessfully, bool userAlreadyExist)
        {
            Code = code;
            CodeSendToEmailSuccessfully = codeSendToEmailSuccessfully;
            UserAlreadyExist = userAlreadyExist;
        }

        public CodeSendEmailUserDTO()
        {
        }
    }
}
