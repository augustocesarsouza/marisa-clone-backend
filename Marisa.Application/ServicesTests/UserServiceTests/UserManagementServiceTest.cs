using FluentValidation.Results;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Domain.Authentication;
using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;
using Moq;
using Xunit;

namespace Marisa.Application.ServicesTests.UserServiceTests
{
    public class UserManagementServiceTest
    {
        private readonly UserManagementServiceConfiguration _userManagementServiceConfiguration;
        private readonly UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            _userManagementServiceConfiguration = new();
            var userManagementService = new UserManagementService(_userManagementServiceConfiguration.UserRepositoryMock.Object,
                _userManagementServiceConfiguration.MapperMock.Object, _userManagementServiceConfiguration.UnitOfWorkMock.Object,
            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Object, _userManagementServiceConfiguration.UserUpdateProfileDTOValidatorMock.Object,
            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Object, _userManagementServiceConfiguration.CloudinaryUtiMock.Object,
            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Object, _userManagementServiceConfiguration.UserChangePasswordTokenDTOValidatorMock.Object,
            _userManagementServiceConfiguration.CodeRandomDictionaryMock.Object,_userManagementServiceConfiguration.QuantityAttemptChangePasswordDictionaryMock.Object,
            _userManagementServiceConfiguration.TokenGeneratorUserMock.Object, _userManagementServiceConfiguration.SendEmailUserMock.Object);
            _userManagementService = userManagementService;
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            UserDTO userDTO = new UserDTO(Guid.NewGuid(), "augusto", "augusto@gmail.com", null, null, 'm', "",
                "", "", "", null);
            userDTO.SetTokenForCreation(333223);
            userDTO.SetPassword("password123");
            userDTO.SetBirthDateString("05/10/1999");

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.CodeRandomDictionaryMock.Setup(valid => valid.Container(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
               .Returns(byteUser);

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
               .Returns("asdcasfcDFOJJIO2323IUIJOZ");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.Create(userDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Validate_DTO_CreateAsync()
        {
            UserDTO userDTO = new UserDTO(Guid.NewGuid(), "augusto", "augusto@gmail.com", null, null, 'm', "",
                "", "", "", null);

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _userManagementService.Create(userDTO);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_Create_Account_Repository_CreateAsync()
        {
            UserDTO userDTO = new UserDTO(Guid.NewGuid(), "augusto", "augusto@gmail.com", null, null, 'm', "",
                "", "", "", null);
            userDTO.SetTokenForCreation(333223);
            userDTO.SetPassword("password123");
            userDTO.SetBirthDateString("05/10/1999");

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.CodeRandomDictionaryMock.Setup(valid => valid.Container(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
               .Returns(byteUser);

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
               .Returns("asdcasfcDFOJJIO2323IUIJOZ");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.Create(userDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create user null value", result.Message);
        }

        [Fact]
        public async Task Should_ChangePassword_Success()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), "password123", "password456");

            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<ChangePasswordUser>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.QuantityAttemptChangePasswordDictionaryMock.Setup(valid => valid.GetKey(It.IsAny<string>()))
            .Returns((1, TimeSpan.FromMinutes(5)));

            var user = new User();
            var passwordHash = "passwordhash";
            user.SetPasswordHash(passwordHash);
            user.SetSalt("salt123");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdToChangePassword(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword("pass", byteUser))
               .Returns(passwordHash);

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
               .Returns(byteUser);

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
              .Returns("asdcasfcDFOJJIO2323IUIJOZ");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.ChangePassword(changePasswordUser);
            var data = result.Data;

            if (data != null)
            {
                Assert.True(data.PasswordChangedSuccessfully);
            }
        }

        [Fact]
        public async Task Should_Throw_Error_DTO_Is_Null_ChangePassword()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), "password123", "password456");

            var result = await _userManagementService.ChangePassword(null);
            Assert.False(result.IsSucess);
            Assert.Equal("userDTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Validate_DTO_ChangePassword()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), null, "password456");

            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<ChangePasswordUser>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));
                
            var result = await _userManagementService.ChangePassword(changePasswordUser);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Try_Get_User_ChangePassword()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), "password123", "password456");

            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<ChangePasswordUser>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.QuantityAttemptChangePasswordDictionaryMock.Setup(valid => valid.GetKey(It.IsAny<string>()))
            .Returns((1, TimeSpan.FromMinutes(5)));

            var user = new User();
            var passwordHash = "passwordhash";
            user.SetPasswordHash(passwordHash);
            user.SetSalt("salt123");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdToChangePassword(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.ChangePassword(changePasswordUser);
            Assert.False(result.IsSucess);
            Assert.Equal("Error user Not Found", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_PasswordHash_Or_Salt_Is_Null_ChangePassword()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), "password123", "password456");

            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<ChangePasswordUser>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.QuantityAttemptChangePasswordDictionaryMock.Setup(valid => valid.GetKey(It.IsAny<string>()))
            .Returns((1, TimeSpan.FromMinutes(5)));

            var user = new User();

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdToChangePassword(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var result = await _userManagementService.ChangePassword(changePasswordUser);
            Assert.False(result.IsSucess);
            Assert.Equal("Error password hash or salt is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Update_User_Repository_ChangePassword()
        {
            var changePasswordUser = new ChangePasswordUser(Guid.NewGuid().ToString(), "password123", "password456");

            _userManagementServiceConfiguration.ChangePasswordUserDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<ChangePasswordUser>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.QuantityAttemptChangePasswordDictionaryMock.Setup(valid => valid.GetKey(It.IsAny<string>()))
                .Returns((1, TimeSpan.FromMinutes(5)));

            var user = new User();
            var passwordHash = "passwordhash";
            string salt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==";
            user.SetPasswordHash(passwordHash);
            user.SetSalt(salt);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdToChangePassword(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
              
            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
               .Returns(byteUser);

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
              .Returns(passwordHash);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.ChangePassword(changePasswordUser);
            Assert.False(result.IsSucess);
            Assert.Equal("Error userUpdate it is null", result.Message);
        }

        [Fact]
        public async Task Should_SendTokenChangePassword_Success()
        {
            string email = "augusto@gmail.com";

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddSeconds(30);
            tokenOutValue.ValidateToken("token1234", expires);

            var infoErrors = InfoErrors.Ok(tokenOutValue);

            _userManagementServiceConfiguration.TokenGeneratorUserMock.Setup(rep => rep.GeneratorTokenUrlChangeEmail(It.IsAny<User>()))
                .Returns(infoErrors);

            var infoErrorsSendEmail = InfoErrors.Ok("tudo certo com o envio do email");

            _userManagementServiceConfiguration.SendEmailUserMock.Setup(rep => rep.SendUrlChangePassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(infoErrorsSendEmail);

            var result = await _userManagementService.SendTokenChangePassword(email);
            var data = result.Data;

            if(data != null)
            {
                Assert.True(result.IsSucess);
                Assert.True(data.TokenSentEmail);
                Assert.Equal("token sent sucessfully", data.Message);
            }
        }

        [Fact]
        public async Task Should_Return_Null_When_Get_User_By_Email_Respository_SendTokenChangePassword()
        {
            string email = "augusto@gmail.com";

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.SendTokenChangePassword(email);
            var data = result.Data;

            if (data != null)
            {
                Assert.False(data.TokenSentEmail);
            }
        }

        [Fact]
        public async Task Should_Error_Send_Email_User_SendTokenChangePassword()
        {
            string email = "augusto@gmail.com";

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddSeconds(30);
            tokenOutValue.ValidateToken("token1234", expires);

            var infoErrors = InfoErrors.Ok(tokenOutValue);

            _userManagementServiceConfiguration.TokenGeneratorUserMock.Setup(rep => rep.GeneratorTokenUrlChangeEmail(It.IsAny<User>()))
                .Returns(infoErrors);

            var infoErrorsSendEmail = InfoErrors.Fail("error send token email");

            _userManagementServiceConfiguration.SendEmailUserMock.Setup(rep => rep.SendUrlChangePassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(infoErrorsSendEmail);

            var result = await _userManagementService.SendTokenChangePassword(email);
            var data = result.Data;

            if (data != null)
            {
                Assert.False(data.TokenSentEmail);
            }
        }
    }
}