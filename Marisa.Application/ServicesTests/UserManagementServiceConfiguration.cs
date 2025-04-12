using AutoMapper;
using Marisa.Application.CodeRandomUser.Interface;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.UtilityExternal.Interface;
using Moq;

namespace Marisa.Application.ServicesTests
{
    public class UserManagementServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IUserCreateDTOValidator> UserCreateDTOValidatorMock { get; }
        public Mock<IUserUpdateProfileDTOValidator> UserUpdateProfileDTOValidatorMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }
        public Mock<IChangePasswordUserDTOValidator> ChangePasswordUserDTOValidatorMock { get; }
        public Mock<IUserChangePasswordTokenDTOValidator> UserChangePasswordTokenDTOValidatorMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }
        public Mock<ICodeRandomDictionary> CodeRandomDictionaryMock { get; }
        public Mock<IQuantityAttemptChangePasswordDictionary> QuantityAttemptChangePasswordDictionaryMock { get; }
        public Mock<ITokenGeneratorUser> TokenGeneratorUserMock { get; }
        public Mock<ISendEmailUser> SendEmailUserMock { get; }

        public UserManagementServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            UserCreateDTOValidatorMock = new();
            UserUpdateProfileDTOValidatorMock = new();
            UserCreateAccountFunctionMock = new();
            ChangePasswordUserDTOValidatorMock = new();
            UserChangePasswordTokenDTOValidatorMock = new();
            CloudinaryUtiMock = new();
            CodeRandomDictionaryMock = new();
            QuantityAttemptChangePasswordDictionaryMock = new();
            TokenGeneratorUserMock = new();
            SendEmailUserMock = new();
        }
    }
}