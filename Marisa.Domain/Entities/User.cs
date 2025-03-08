

namespace Marisa.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Cpf { get; private set; }
        public char Gender { get; private set; }
        public string CellPhone { get; private set; }
        public string Telephone { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public string UserImage { get; private set; }

        public User(Guid id, string name, string email, DateTime birthDate, string cpf, char gender, string cellPhone, 
            string telephone, string passwordHash, string salt, string userImage)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Cpf = cpf;
            Gender = gender;
            CellPhone = cellPhone;
            Telephone = telephone;
            PasswordHash = passwordHash;
            Salt = salt;
            UserImage = userImage;
        }

        public User()
        {
        }

        public Guid GetId()
        {
            return Id;
        }

        public string GetName() => Name;
        public string GetEmail() => Email;
        public DateTime GetBirthDate() => BirthDate;
        public string GetCpf() => Cpf;
        public char GetGender() => Gender;
        public string GetCellPhone() => CellPhone;
        public string GetTelephone() => Telephone;
        public string GetPasswordHash() => PasswordHash;
        public string GetSalt() => Salt;
        public string GetUserImage() => UserImage;

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetSalt(string salt)
        {
            Salt = salt;
        }
    }
}
