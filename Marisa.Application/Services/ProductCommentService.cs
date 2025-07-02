using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;

namespace Marisa.Application.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCommentCreateDTOValidator _productCommentCreateDTOValidator;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IProductService _productService;

        public ProductCommentService(IProductCommentRepository productCommentRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IProductCommentCreateDTOValidator productCommentCreateDTOValidator, IUserAuthenticationService userAuthenticationService,
            IProductService productService)
        {
            _productCommentRepository = productCommentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productCommentCreateDTOValidator = productCommentCreateDTOValidator;
            _userAuthenticationService = userAuthenticationService;
            _productService = productService;
        }

        public async Task<ResultService<ProductCommentDTO>> GetProductCommentById(Guid productCommentId)
        {
            try
            {
                var ProductComment = await _productCommentRepository.GetProductCommentById(productCommentId);

                if (ProductComment == null)
                    return ResultService.Fail<ProductCommentDTO>("Error ProductComment null");

                return ResultService.Ok(_mapper.Map<ProductCommentDTO>(ProductComment));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductCommentDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductCommentDTO>>> GetAllProductCommentByUserIdAndProductId(Guid productId)
        {
            try
            {
                var ProductComments = await _productCommentRepository.GetAllProductCommentByUserIdAndProductId(productId);

                if (ProductComments == null)
                    return ResultService.Fail<List<ProductCommentDTO>>("Error ProductComment list null");

                return ResultService.Ok(_mapper.Map<List<ProductCommentDTO>>(ProductComments));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductCommentDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductCommentDTO>> Create(ProductCommentCreate? productCommentCreate)
        {
            if (productCommentCreate == null)
                return ResultService.Fail<ProductCommentDTO>("userDTO is null");

            var validation = _productCommentCreateDTOValidator.ValidateDTO(productCommentCreate);

            if (!validation.IsValid)
                return ResultService.RequestError<ProductCommentDTO>("validation error check the information", validation);

            try
            {
                await _unitOfWork.BeginTransaction();

                var productId = productCommentCreate.ProductId ?? "";

                var ifProductExist = await _productService.GetProductIfExist(productId);

                if (!ifProductExist.IsSucess)
                    return ResultService.Fail<ProductCommentDTO>("error user not found");

                var productIdGuid = Guid.Parse(productId);
                Guid id = Guid.NewGuid();

                var productComment = new ProductComment(id, productIdGuid, null, productCommentCreate.StarQuantity, productCommentCreate.RecommendProduct,
                    productCommentCreate.Comment, productCommentCreate.Name, productCommentCreate.Email, productCommentCreate.ImgProduct, DateTime.UtcNow, DateTime.UtcNow);

                var productCommentCreateHere = await _productCommentRepository.CreateAsync(productComment);

                if (productCommentCreateHere == null)
                    return ResultService.Fail<ProductCommentDTO>("error when create ProductComment null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductCommentDTO>(productCommentCreateHere));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductCommentDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductCommentDTO>> Delete(Guid productCommentId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var productComment = await _productCommentRepository.GetProductCommentByIdToDelete(productCommentId);

                if (productComment == null)
                    return ResultService.Fail<ProductCommentDTO>("error productComment is null");

                var deleteEntity = await _productCommentRepository.DeleteAsync(productComment);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<ProductCommentDTO>(deleteEntity));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductCommentDTO>($"{ex.Message}");
            }
        }
    }
}
