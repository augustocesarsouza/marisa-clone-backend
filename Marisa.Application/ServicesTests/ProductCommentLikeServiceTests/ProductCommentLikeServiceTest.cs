using FluentValidation.Results;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Enums;
using Moq;
using Xunit;

namespace Marisa.Application.ServicesTests.ProductCommentLikeServiceTests
{
    public class ProductCommentLikeServiceTest
    {
        private readonly ProductCommentLikeServiceConfiguration _productCommentLikeServiceConfiguration;
        private readonly ProductCommentLikeService _productCommentLikeService;

        public ProductCommentLikeServiceTest()
        {
            _productCommentLikeServiceConfiguration = new();
            var productCommentLikeService = new ProductCommentLikeService(_productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock.Object,
                _productCommentLikeServiceConfiguration.UserAuthenticationServiceMock.Object, _productCommentLikeServiceConfiguration.ProductCommentServiceMock.Object,
                _productCommentLikeServiceConfiguration.MapperMock.Object, _productCommentLikeServiceConfiguration.UnitOfWorkMock.Object,
                _productCommentLikeServiceConfiguration.ProductCommentLikeCreateDTOValidatorMock.Object);
            _productCommentLikeService = productCommentLikeService;
        }

        [Fact]
        public async Task Should_GetProductCommentLikeById_Success()
        {
            var productCommentLikeId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock.Setup(rep => rep.GetProductCommentLikeById(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductCommentLike());

            var result = await _productCommentLikeService.GetProductCommentLikeById(Guid.Parse(productCommentLikeId));
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_GetProductCommentLikeById()
        {
            var productCommentLikeId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock.Setup(rep => rep.GetProductCommentLikeById(It.IsAny<Guid>()))
                .ReturnsAsync((ProductCommentLike?)null);

            var result = await _productCommentLikeService.GetProductCommentLikeById(Guid.Parse(productCommentLikeId));
            Assert.False(result.IsSucess);
            Assert.Equal("Error ProductCommentLike null", result.Message);
        }

        [Fact]
        public async Task Should_DeleteAsync_Success()
        {
            var id = Guid.NewGuid();

            var productCommentLikeCreateDTO = new ProductCommentLikeCreate("a93daeb8-7c74-481e-9b08-022f3586eb89",
               "67bcefd0-eb3c-4a45-9e53-26fda90911a4", "a4dcdcf6-1a65-4347-95e9-fe55e6ca2fe8", ReactionType.None);

            _productCommentLikeServiceConfiguration.ProductCommentLikeCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductCommentLikeCreate>()))
                .Returns(new ValidationResult());

            _productCommentLikeServiceConfiguration.UserAuthenticationServiceMock
                .Setup(valid => valid.GetUserByIdJustToCheckIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new UserDTO()));

            _productCommentLikeServiceConfiguration.ProductCommentServiceMock
                 .Setup(valid => valid.GetProductCommentByIdIfExist(It.IsAny<Guid>()))
                 .ReturnsAsync(ResultService.Ok(new ProductCommentDTO()));

            //_productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock
            //    .Setup(rep => rep.GetByProductCommentIdAndUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
            //    .ReturnsAsync(new ProductCommentLike());

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock
                .Setup(rep => rep.GetByProductCommentIdAndUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new ProductCommentLike());

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock
                .Setup(rep => rep.DeleteAsync(It.IsAny<ProductCommentLike>()))
                .ReturnsAsync(new ProductCommentLike());

            var result = await _productCommentLikeService.Create(productCommentLikeCreateDTO);
            Assert.True(result.IsSucess);
            Assert.Equal(result.Data?.Reaction, ReactionType.None);
        }

        [Fact]
        public async Task Should_CreateAsync_Successd()
        {
            var id = Guid.NewGuid();

            var productCommentLikeCreateDTO = new ProductCommentLikeCreate("a93daeb8-7c74-481e-9b08-022f3586eb89",
               "67bcefd0-eb3c-4a45-9e53-26fda90911a4", "a4dcdcf6-1a65-4347-95e9-fe55e6ca2fe8", ReactionType.None);

            _productCommentLikeServiceConfiguration.ProductCommentLikeCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductCommentLikeCreate>()))
                .Returns(new ValidationResult());

            _productCommentLikeServiceConfiguration.UserAuthenticationServiceMock
                .Setup(valid => valid.GetUserByIdJustToCheckIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new UserDTO()));

            _productCommentLikeServiceConfiguration.ProductCommentServiceMock
                 .Setup(valid => valid.GetProductCommentByIdIfExist(It.IsAny<Guid>()))
                 .ReturnsAsync(ResultService.Ok(new ProductCommentDTO()));

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock
                .Setup(rep => rep.GetByProductCommentIdAndUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((ProductCommentLike?)null);

            _productCommentLikeServiceConfiguration.ProductCommentLikeRespositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductCommentLike>()))
                .ReturnsAsync(new ProductCommentLike());

            var result = await _productCommentLikeService.Create(productCommentLikeCreateDTO);
            Assert.True(result.IsSucess);
            Assert.Equal(result.Data?.Reaction, ReactionType.Like);
        }
    }
}
