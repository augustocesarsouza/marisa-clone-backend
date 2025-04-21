namespace Marisa.Application.DTOs
{
    public class AddressConfirmCodeEmailDTO
    {
        public bool CodeIsCorrect { get; set; }
        //public bool PasswordChangedSuccessfully { get; set; }
        public int NumberOfAttempts { get; set; }
        public TimeSpan? TimeRemaining { get; set; }

        public AddressConfirmCodeEmailDTO() { }

        public AddressConfirmCodeEmailDTO(bool codeIsCorrect, int numberOfAttempts, TimeSpan? timeRemaining)
        {
            CodeIsCorrect = codeIsCorrect;
            NumberOfAttempts = numberOfAttempts;
            TimeRemaining = timeRemaining;
        }
    }
}
