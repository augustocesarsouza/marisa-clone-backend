using FluentValidation.Results;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Moq;
using Xunit;

namespace Marisa.Application.ServicesTests.ProductCommentServiceTests
{
    public class ProductCommentServiceTest
    {
        private readonly ProductCommentServiceConfiguration _productCommentServiceConfiguration;
        private readonly ProductCommentService _productCommentService;

        public ProductCommentServiceTest()
        {
            _productCommentServiceConfiguration = new();
            var productCommentService = new ProductCommentService(_productCommentServiceConfiguration.ProductCommentRepositoryMock.Object,
                _productCommentServiceConfiguration.MapperMock.Object, _productCommentServiceConfiguration.UnitOfWorkMock.Object,
                _productCommentServiceConfiguration.ProductCommentCreateDTOValidatorMock.Object, _productCommentServiceConfiguration.UserAuthenticationServiceMock.Object,
                _productCommentServiceConfiguration.ProductServiceMock.Object);
            _productCommentService = productCommentService;
        }

        [Fact]
        public async Task Should_GetProductCommentById_Success()
        {
            var productCommentId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock.Setup(rep => rep.GetProductCommentById(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductComment());

            var result = await _productCommentService.GetProductCommentById(Guid.Parse(productCommentId));
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_GetProductCommentById()
        {
            var productCommentId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock.Setup(rep => rep.GetProductCommentById(It.IsAny<Guid>()))
                .ReturnsAsync((ProductComment?)null);

            var result = await _productCommentService.GetProductCommentById(Guid.Parse(productCommentId));
            Assert.False(result.IsSucess);
            Assert.Equal("Error ProductComment null", result.Message);
        }

        [Fact]
        public async Task Should_GetAllProductCommentByUserIdAndProductId_Success()
        {
            var userId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";
            var productId = "b2488ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.GetAllProductCommentByUserIdAndProductId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ProductComment>());

            var result = await _productCommentService
                .GetAllProductCommentByUserIdAndProductId( Guid.Parse(productId));
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_When_GetAllProductCommentByUserIdAndProductId()
        {
            var userId = "b8088ca3-f8de-49d9-ba28-42ad3c041929";
            var productId = "b2488ca3-f8de-49d9-ba28-42ad3c041929";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.GetAllProductCommentByUserIdAndProductId(It.IsAny<Guid>()))
                .ReturnsAsync((List<ProductComment>?)null);

            var result = await _productCommentService
                .GetAllProductCommentByUserIdAndProductId(Guid.Parse(productId));
            Assert.False(result.IsSucess);
            Assert.Equal("Error ProductComment list null", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var id = Guid.NewGuid();
            var productId = "b2488ca3-f8de-49d9-ba28-42ad3c041929";

            var productCommentDTO = new ProductCommentCreate(id.ToString(), null, null, "comment here", null, null, null);
            productCommentDTO.SetProductIdString(productId);

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            _productCommentServiceConfiguration.ProductCommentCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductCommentCreate>()))
                .Returns(new ValidationResult());

            _productCommentServiceConfiguration.UserAuthenticationServiceMock
                .Setup(rep => rep.GetUserByIdJustToCheckIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new UserDTO()));

            _productCommentServiceConfiguration.ProductServiceMock
                .Setup(rep => rep.GetProductIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new ProductDTO()));

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductComment>()))
                .ReturnsAsync(new ProductComment());

            var result = await _productCommentService.Create(productCommentDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_CreateAsync_Validate_ProductCommentDTO()
        {
            var id = Guid.NewGuid();
            var productCommentDTO = new ProductCommentCreate(id.ToString(), null, null, "comment here", null, null, null);

            _productCommentServiceConfiguration.ProductCommentCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductCommentCreate>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _productCommentService.Create(productCommentDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_Exception_CreateAsync()
        {
            var id = Guid.NewGuid();
            var productId = "b2488ca3-f8de-49d9-ba28-42ad3c041929";

            var productCommentDTO = new ProductCommentCreate(id.ToString(), null, null, "comment here", null, null, null);
            productCommentDTO.SetProductIdString(productId);

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            _productCommentServiceConfiguration.ProductCommentCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductCommentCreate>()))
                .Returns(new ValidationResult());

            _productCommentServiceConfiguration.UserAuthenticationServiceMock
                .Setup(rep => rep.GetUserByIdJustToCheckIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new UserDTO()));

            _productCommentServiceConfiguration.ProductServiceMock
                .Setup(rep => rep.GetProductIfExist(It.IsAny<string>()))
                .ReturnsAsync(ResultService.Ok(new ProductDTO()));

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductComment>()))
                .ThrowsAsync(new Exception("Erro ao criar um ProductComment"));

            var result = await _productCommentService.Create(productCommentDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao criar um ProductComment", result.Message);
        }

        [Fact]
        public async Task Should_Delete_Success()
        {
            var productCommentId = "b8088ca3-f8de-49d9-ba28-42ad3c742529";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.GetProductCommentByIdToDelete(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductComment());

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductComment>()))
                .ReturnsAsync(new ProductComment());

            var result = await _productCommentService.Delete(Guid.Parse(productCommentId));
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_Exception_When_Delete()
        {
            var productCommentId = "b8088ca3-f8de-49d9-ba28-42ad3c742529";

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.GetProductCommentByIdToDelete(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductComment());

            _productCommentServiceConfiguration.ProductCommentRepositoryMock
                .Setup(rep => rep.DeleteAsync(It.IsAny<ProductComment>()))
                .ThrowsAsync(new Exception("Erro ao deletar um ProductComment"));

            var result = await _productCommentService.Delete(Guid.Parse(productCommentId));

            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao deletar um ProductComment", result.Message);
        }
    }
}