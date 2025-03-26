namespace Marisa.Application.DTOs
{
    public class ChangePasswordUserReturnDTO
    {
        public bool PasswordIsCorrect { get; set; }
        public bool PasswordChangedSuccessfully { get; set; }
        public int NumberOfAttempts { get; set; }
        public TimeSpan? TimeRemaining { get; set; }

        public ChangePasswordUserReturnDTO()
        {
        }

        public ChangePasswordUserReturnDTO(bool passwordIsCorrect, bool passwordChangedSuccessfully, int numberOfAttempts, TimeSpan? timeRemaining)
        {
            PasswordIsCorrect = passwordIsCorrect;
            PasswordChangedSuccessfully = passwordChangedSuccessfully;
            NumberOfAttempts = numberOfAttempts;
            TimeRemaining = timeRemaining;
        }
    }
}
