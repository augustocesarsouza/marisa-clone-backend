using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Enums;
using Marisa.Domain.Repositories;

namespace Marisa.Application.Services
{
    public class ProductCommentLikeService : IProductCommentLikeService
    {
        private readonly IProductCommentLikeRespository _productCommentLikeRespository;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IProductCommentService _productCommentService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCommentLikeCreateDTOValidator _productCommentLikeCreateDTOValidator;

        public ProductCommentLikeService(IProductCommentLikeRespository productCommentLikeRespository,
            IUserAuthenticationService userAuthenticationService,
            IProductCommentService productCommentService,
            IMapper mapper, IUnitOfWork unitOfWork,
            IProductCommentLikeCreateDTOValidator productCommentLikeCreateDTOValidator)
        {
            _productCommentLikeRespository = productCommentLikeRespository;
            _userAuthenticationService = userAuthenticationService;
            _productCommentService = productCommentService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productCommentLikeCreateDTOValidator = productCommentLikeCreateDTOValidator;
        }

        public async Task<ResultService<ProductCommentLikeDTO>> GetProductCommentLikeById(Guid? productCommentLikeId)
        {
            try
            {
                var productCommentLike = await _productCommentLikeRespository.GetProductCommentLikeById(productCommentLikeId);

                if (productCommentLike == null)
                    return ResultService.Fail<ProductCommentLikeDTO>("Error ProductCommentLike null");

                return ResultService.Ok(_mapper.Map<ProductCommentLikeDTO>(productCommentLike));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductCommentLikeDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductCommentLikeDTO>>> GetAllProductCommentLikeByProductId(Guid? productId)
        {
            try
            {
                var productCommentLikes = await _productCommentLikeRespository.GetAllProductCommentLikeByProductId(productId);

                if (productCommentLikes == null)
                    return ResultService.Fail<List<ProductCommentLikeDTO>>("Error ProductCommentLikes null");

                return ResultService.Ok(_mapper.Map<List<ProductCommentLikeDTO>>(productCommentLikes));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductCommentLikeDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductCommentLikeCreateOrDelete>> Create(ProductCommentLikeCreate? productCommentLikeCreate)
        {
            if (productCommentLikeCreate == null)
                return ResultService.Fail<ProductCommentLikeCreateOrDelete>("productCommentLikeCreate is null");

            var validation = _productCommentLikeCreateDTOValidator.ValidateDTO(productCommentLikeCreate);

            if (!validation.IsValid)
                return ResultService.RequestError<ProductCommentLikeCreateOrDelete>("validation error check the information", validation);

            try
            {
                await _unitOfWork.BeginTransaction();

                var userId = productCommentLikeCreate.UserId ?? "";
                var productCommentId = productCommentLikeCreate.ProductCommentId ?? "";
                var productId = productCommentLikeCreate.ProductId ?? "";

                var productCommentIdGuid = Guid.Parse(productCommentId);
                var userIdGuid = Guid.Parse(userId);
                var productIdGuid = Guid.Parse(productId);

                var ifUserExist = await _userAuthenticationService.GetUserByIdJustToCheckIfExist(userId);

                if (!ifUserExist.IsSucess)
                    return ResultService.Fail<ProductCommentLikeCreateOrDelete>(ifUserExist.Message ?? "error get user");

                var ifProductCommentExist = await _productCommentService.GetProductCommentByIdIfExist(productCommentIdGuid);

                if (!ifProductCommentExist.IsSucess)
                    return ResultService.Fail<ProductCommentLikeCreateOrDelete>(ifProductCommentExist.Message ?? "error get product comment");

                var productCommentLikeIfExist = await _productCommentLikeRespository.GetByProductCommentIdAndUser(productCommentIdGuid, userIdGuid);

                if (productCommentLikeIfExist != null)
                {
                    if (productCommentLikeCreate.Reaction == ReactionType.None)
                    {
                        var deleteProductCommentLike = await _productCommentLikeRespository.DeleteAsync(productCommentLikeIfExist);

                        if (deleteProductCommentLike == null)
                            return ResultService.Fail<ProductCommentLikeCreateOrDelete>("error when delete ProductCommentLike");

                        await _unitOfWork.Commit();

                        return ResultService.Ok(new ProductCommentLikeCreateOrDelete(ReactionType.None));
                    }

                    if (productCommentLikeCreate.Reaction == ReactionType.Dislike)
                    {
                        productCommentLikeIfExist.SetReaction(ReactionType.Dislike);
                    }

                    if (productCommentLikeCreate.Reaction == ReactionType.Like)
                    {
                        productCommentLikeIfExist.SetReaction(ReactionType.Like);
                    }

                    var updateProductCommentLike = await _productCommentLikeRespository.UpdateAsync(productCommentLikeIfExist);

                    if (updateProductCommentLike == null)
                        return ResultService.Fail<ProductCommentLikeCreateOrDelete>("error when update ProductCommentLike");

                    await _unitOfWork.Commit();

                    return ResultService.Ok(new ProductCommentLikeCreateOrDelete(ReactionType.Dislike));
                }

                var likeOrDislike = ReactionType.Like;

                if (productCommentLikeCreate.Reaction == ReactionType.Dislike)
                {
                    likeOrDislike = ReactionType.Dislike;
                }

                Guid id = Guid.NewGuid();

                var productCommentLike = new ProductCommentLike(id, productIdGuid, null, Guid.Parse(userId), null, productCommentIdGuid, null,
                    likeOrDislike, DateTime.UtcNow);

                var productCommentCreateHere = await _productCommentLikeRespository.CreateAsync(productCommentLike);

                if (productCommentCreateHere == null)
                    return ResultService.Fail<ProductCommentLikeCreateOrDelete>("error when create ProductCommentLike null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(new ProductCommentLikeCreateOrDelete(likeOrDislike));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductCommentLikeCreateOrDelete>(ex.Message);
            }
        }
    }
}
