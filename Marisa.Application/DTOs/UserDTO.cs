namespace Marisa.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; }
        public char Gender { get; set; }
        public string CellPhone { get; set; }
        public string Telephone { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string UserImage { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string BirthDateString { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(Guid id, string name, string email, DateTime birthDate, string cpf, char gender, 
            string cellPhone, string telephone, string passwordHash, string salt, string userImage)
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

        public Guid SetId(Guid id) => Id = id;
        public string SetName(string name) => Name = name;
        public string SetEmail(string email) => Email = email;
        public DateTime SetBirthDate(DateTime birthDate) => BirthDate = birthDate;
        public string SetCpf(string cpf) => Cpf = cpf;
        public char SetGender(char gender) => Gender = gender;
        public string SetCellPhone(string cellPhone) => CellPhone = cellPhone;
        public string SetTelephone(string telephone) => Telephone = telephone;
        public string SetPasswordHash(string passwordHash) => PasswordHash = passwordHash;
        public string SetSalt(string salt) => Salt = salt;
        public string SetUserImage(string userImage) => UserImage = userImage;
        public string SetToken(string token) => Token = token;
    }
}
