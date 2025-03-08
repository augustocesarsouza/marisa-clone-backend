namespace Marisa.Application.Services.Interfaces
{
    public interface IUserCreateAccountFunction
    {
        public string HashPassword(string password, byte[] salt);
        public byte[] GenerateSalt();
    }
}
