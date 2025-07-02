using AutoMapper;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Repositories;
using Moq;

namespace Marisa.Application.ServicesTests
{
    public class ProductCommentServiceConfiguration
    {
        public Mock<IProductCommentRepository> ProductCommentRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IProductCommentCreateDTOValidator> ProductCommentCreateDTOValidatorMock { get; }
        public Mock<IUserAuthenticationService> UserAuthenticationServiceMock { get; }
        public Mock<IProductService> ProductServiceMock { get; }

        public ProductCommentServiceConfiguration()
        {
            ProductCommentRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            ProductCommentCreateDTOValidatorMock = new();
            UserAuthenticationServiceMock = new();
            ProductServiceMock = new();
        }
    }
}
