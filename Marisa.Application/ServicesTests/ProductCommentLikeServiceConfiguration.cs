using AutoMapper;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Repositories;
using Moq;

namespace Marisa.Application.ServicesTests
{
    public class ProductCommentLikeServiceConfiguration
    {
        public Mock<IProductCommentLikeRespository> ProductCommentLikeRespositoryMock { get; }
        public Mock<IUserAuthenticationService> UserAuthenticationServiceMock { get; }
        public Mock<IProductCommentService> ProductCommentServiceMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IProductCommentLikeCreateDTOValidator> ProductCommentLikeCreateDTOValidatorMock { get; }

        public ProductCommentLikeServiceConfiguration()
        {
            ProductCommentLikeRespositoryMock = new();
            UserAuthenticationServiceMock = new();
            ProductCommentServiceMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            ProductCommentLikeCreateDTOValidatorMock = new();
        }
    }
}