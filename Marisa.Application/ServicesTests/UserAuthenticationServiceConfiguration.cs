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
    public class UserAuthenticationServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ITokenGeneratorUser> TokenGeneratorUserMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }
        public Mock<ISendEmailUser> SendEmailUserMock { get; }
        public Mock<IUserSendCodeEmailDTOValidator> UserSendCodeEmailDTOValidatorMock { get; }
        public Mock<ICodeRandomDictionary> CodeRandomDictionaryMock { get; }

        public UserAuthenticationServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            TokenGeneratorUserMock = new();
            UserCreateAccountFunctionMock = new();
            SendEmailUserMock = new();
            UserSendCodeEmailDTOValidatorMock = new();
            CodeRandomDictionaryMock = new();
        }
    }
}
