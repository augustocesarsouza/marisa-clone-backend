using System.Threading.Tasks;
using FluentValidation.Results;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Marisa.Application.ServicesTests.UserServiceTests
{
    public class UserAuthenticationServiceTest
    {
        private readonly UserAuthenticationServiceConfiguration _userAuthenticationServiceConfiguration;
        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTest()
        {
            _userAuthenticationServiceConfiguration = new();
            var userAuthenticationService = new UserAuthenticationService(_userAuthenticationServiceConfiguration.UserRepositoryMock.Object,
                _userAuthenticationServiceConfiguration.MapperMock.Object, _userAuthenticationServiceConfiguration.UnitOfWorkMock.Object,
                _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Object, _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Object,
                _userAuthenticationServiceConfiguration.SendEmailUserMock.Object, _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Object,
                _userAuthenticationServiceConfiguration.CodeRandomDictionaryMock.Object);
            _userAuthenticationService = userAuthenticationService;
        }

        [Fact]
        public async Task Should_GetByIdInfoUser_Success()
        {
            var userId = "05c82c6e-4c39-4e47-a074-b22862eb7aad";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdInfoUser(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.GetByIdInfoUser(userId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_Repository_GetUserByIdInfoUser_GetByIdInfoUser()
        {
            var userId = "05c82c6e-4c39-4e47-a074-b22862eb7aad";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdInfoUser(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);
             
            var result = await _userAuthenticationService.GetByIdInfoUser(userId);
            Assert.False(result.IsSucess);
            Assert.Equal("Error user null", result.Message);
        }

        [Fact]
        public async Task Should_GetInfoToUpdateProfile_Success()
        {
            var userId = "05c82c6e-4c39-4e47-a074-b22862eb7aad";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetInfoToUpdateProfile(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.GetInfoToUpdateProfile(userId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_Repository_GetInfoToUpdateProfile()
        {
            var userId = "05c82c6e-4c39-4e47-a074-b22862eb7aad";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetInfoToUpdateProfile(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.GetInfoToUpdateProfile(userId);
            Assert.False(result.IsSucess);
            Assert.Equal("Error user null", result.Message);
        }

        [Fact]
        public async Task Should_Login_Successfully()
        {
            var email = "augusto@gmail.com";
            var password = "password123";

            var user = new User();
            var passwordHash = "passwordhash";
            user.SetPasswordHash(passwordHash);
            user.SetSalt("YW55IGNhcm5hbCBwbGVhc3VyZQ==");

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddSeconds(30);
            tokenOutValue.ValidateToken("token1234", expires);

            var infoErrors = InfoErrors.Ok(tokenOutValue);

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
               .Returns(passwordHash);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Setup(valid => valid.Generator(It.IsAny<User>()))
               .Returns(infoErrors);

            var result = await _userAuthenticationService.Login(email, password);
            var data = result.Data;

            if(data != null)
            {
                Assert.True(data.PasswordIsCorrect);
            }
        }

        [Fact]
        public async Task Should_Return_Null_When_GetUserInfoToLogin_Login()
        {
            var email = "augusto@gmail.com";
            var password = "password123";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.Login(email, password);
            var data = result.Data;

            if (data != null)
            {
                Assert.False(data.PasswordIsCorrect);
            }
        }

        [Fact]
        public async Task Should__Login()
        {
            var email = "augusto@gmail.com";
            var password = "password123";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.Login(email, password);
            Assert.False(result.IsSucess);
            Assert.Equal("Error password hash or salt is null", result.Message);
        }

        [Fact]
        public async Task Should_Error_Generate_Token_Login()
        {
            var email = "augusto@gmail.com";
            var password = "password123";

            var user = new User();
            var passwordHash = "passwordhash";
            user.SetPasswordHash(passwordHash);
            user.SetSalt("YW55IGNhcm5hbCBwbGVhc3VyZQ==");

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddSeconds(30);
            tokenOutValue.ValidateToken("token1234", expires);

            var infoErrors = InfoErrors.Fail(tokenOutValue, "error when creating token");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
               .Returns(passwordHash);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Setup(valid => valid.Generator(It.IsAny<User>()))
               .Returns(infoErrors);

            var result = await _userAuthenticationService.Login(email, password);
            Assert.False(result.IsSucess);
            Assert.Equal("error when creating token", result.Message);
        }

        [Fact]
        public void Should_SendCodeEmail_Successfully()
        {
            var userDTO = new UserDTO();
            userDTO.SetEmail("augusto@gmail.com");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
               .Returns(new ValidationResult());

            var tokenOutValue = new TokenOutValue();

            var infoErrors = InfoErrors.Ok(tokenOutValue);

            _userAuthenticationServiceConfiguration.SendEmailUserMock.Setup(valid => valid.SendCodeRandom(It.IsAny<User>(), It.IsAny<int>()))
               .Returns(infoErrors);

            var result = _userAuthenticationService.SendCodeEmail(userDTO);
            var data = result.Data;

            if (data != null)
            {
                Assert.True(data.CodeSendToEmailSuccessfully);
            }
        }

        [Fact]
        public void Should_Throw_Error_DTO_Null_SendCodeEmail()
        {
            var result = _userAuthenticationService.SendCodeEmail(null);
            Assert.False(result.IsSucess);
            Assert.Equal("Error DTO user null", result.Message);
        }

        [Fact]
        public void Should_Throw_Error_When_Validate_DTO_SendCodeEmail()
        {
            var userDTO = new UserDTO();
            userDTO.SetEmail("augusto@gmail.com");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
               .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = _userAuthenticationService.SendCodeEmail(userDTO);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public void Should_Throw_Error_Email_Is_Null_SendCodeEmail()
        {
            var userDTO = new UserDTO();

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
               .Returns(new ValidationResult());

            var tokenOutValue = new TokenOutValue();

            var infoErrors = InfoErrors.Ok(tokenOutValue);

            _userAuthenticationServiceConfiguration.SendEmailUserMock.Setup(valid => valid.SendCodeRandom(It.IsAny<User>(), It.IsAny<int>()))
               .Returns(infoErrors);

            var result = _userAuthenticationService.SendCodeEmail(userDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("User Not Provided Email", result.Message);
        }

        [Fact]
        public async Task Should_VerifyCodeEmail_Successfully()
        {
            var userConfirmCodeEmailDTO = new UserConfirmCodeEmailDTO("434845", "6251c78d-78a2-4495-ae38-eaffb1c990d8", "augusto@gmail.com", true);

            _userAuthenticationServiceConfiguration.CodeRandomDictionaryMock.Setup(rep => rep.Container(It.IsAny<string>(), It.IsAny<int>()))
               .Returns(true);

            var result = await _userAuthenticationService.VerifyCodeEmail(userConfirmCodeEmailDTO);
            var data = result.Data; 
             
            if (data != null)
            {
                Assert.True(data.CorrectCode);
            }
        }

        [Fact]
        public async Task Should_Code_Is_Null_VerifyCodeEmail()
        {
            var userConfirmCodeEmailDTO = new UserConfirmCodeEmailDTO(null, "6251c78d-78a2-4495-ae38-eaffb1c990d8", "augusto@gmail.com", true);

            _userAuthenticationServiceConfiguration.CodeRandomDictionaryMock.Setup(rep => rep.Container(It.IsAny<string>(), It.IsAny<int>()))
               .Returns(true);

            var result = await _userAuthenticationService.VerifyCodeEmail(userConfirmCodeEmailDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("DTO property Is Null", result.Message);
        }

        [Fact]
        public async Task Should_Email_Is_Null_VerifyCodeEmail()
        {
            var userConfirmCodeEmailDTO = new UserConfirmCodeEmailDTO("434845", "6251c78d-78a2-4495-ae38-eaffb1c990d8", null, true);

            _userAuthenticationServiceConfiguration.CodeRandomDictionaryMock.Setup(rep => rep.Container(It.IsAny<string>(), It.IsAny<int>()))
               .Returns(true);

            var result = await _userAuthenticationService.VerifyCodeEmail(userConfirmCodeEmailDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("DTO property Is Null", result.Message);
        }
    }
}
//UserConfirmCodeEmailDTO(string ? code, string ? userId, string ? email, bool correctCode)