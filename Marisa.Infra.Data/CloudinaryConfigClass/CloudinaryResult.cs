namespace Marisa.Infra.Data.CloudinaryConfigClass
{
    public class CloudinaryResult
    {
        public bool DeleteSuccessfully { get; set; }
        public bool CreateSuccessfully { get; set; }
        public string? Message { get; set; }

        public CloudinaryResult(bool deleteSuccessfully, bool createSuccessfully, string? message)
        {
            DeleteSuccessfully = deleteSuccessfully;
            CreateSuccessfully = createSuccessfully;
            Message = message;
        }

        public CloudinaryResult()
        {
        }
    }
}
