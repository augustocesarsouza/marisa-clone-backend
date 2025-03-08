using Marisa.Application.Services.Interfaces;
using System.Text;
using XSystem.Security.Cryptography;

namespace Marisa.Application.Services
{
    public class UserCreateAccountFunction : IUserCreateAccountFunction
    {
        public string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }

        public byte[] GenerateSalt()
        {
            //using (var rng = new RNGCryptoServiceProvider())
            //{
            //    byte[] salt = new byte[16]; // Adjust the size based on your security requirements
            //    rng.GetBytes(salt);
            //    return salt;
            //}

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[32];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }
    }
}
