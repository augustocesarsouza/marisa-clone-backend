using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;

namespace Marisa.Application.Services
{
    public class UserProductLikeService : IUserProductLikeService
    {
        private readonly IUserProductLikeRepository _userProductLikeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProductLikeCreateDTOValidator _userProductLikeCreateDTOValidator;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IProductService _productService;

        public UserProductLikeService(IUserProductLikeRepository userProductLikeRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IUserProductLikeCreateDTOValidator userProductLikeCreateDTOValidator, IUserAuthenticationService userAuthenticationService, 
            IProductService productService)
        {
            _userProductLikeRepository = userProductLikeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userProductLikeCreateDTOValidator = userProductLikeCreateDTOValidator;
            _userAuthenticationService = userAuthenticationService;
            _productService = productService;
        }

        public async Task<ResultService<UserProductLikeDTO>> GetUserProductLikeById(Guid? userProductLikeId)
        {
            try
            {
                var userProductLikeDTO = await _userProductLikeRepository.GetUserProductLikeById(userProductLikeId);

                if (userProductLikeDTO == null)
                    return ResultService.Fail<UserProductLikeDTO>("Error UserProductLike null");

                return ResultService.Ok(_mapper.Map<UserProductLikeDTO>(userProductLikeDTO));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserProductLikeDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserProductLikeDTO>> GetUserProductLikeByProductId(Guid? productId, Guid? userId)
        {
            try
            {
                var userProductLikeDTO = await _userProductLikeRepository.GetUserProductLikeByProductId(productId, userId);

                if (userProductLikeDTO == null)
                    return ResultService.Ok<UserProductLikeDTO>("object not found");

                return ResultService.Ok(_mapper.Map<UserProductLikeDTO>(userProductLikeDTO));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserProductLikeDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<UserProductLikeDTO>>> GetAllUserProductLike()
        {
            try
            {
                var userProductLikeDTOs = await _userProductLikeRepository.GetAllUserProductLike();

                if (userProductLikeDTOs == null)
                    return ResultService.Fail<List<UserProductLikeDTO>>("Error UserProductLike null");

                return ResultService.Ok(_mapper.Map<List<UserProductLikeDTO>>(userProductLikeDTOs));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<UserProductLikeDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<UserProductLikeCreateOrDeleteDTO>> Create(UserProductLikeDTO? userProductLikeDTO)
        {
            if (userProductLikeDTO == null)
                return ResultService.Fail<UserProductLikeCreateOrDeleteDTO>("userDTO is null");

            var validation = _userProductLikeCreateDTOValidator.ValidateDTO(userProductLikeDTO);

            if (!validation.IsValid)
                return ResultService.RequestError<UserProductLikeCreateOrDeleteDTO>("validation error check the information", validation);

            try
            {
                await _unitOfWork.BeginTransaction();

                var productIdString = userProductLikeDTO.ProductIdString ?? "";
                var userIdString = userProductLikeDTO.UserIdString ?? "";

                var ifUserExist = _userAuthenticationService.GetUserByIdJustToCheckIfExist(userIdString);

                if(ifUserExist == null)
                    return ResultService.Fail<UserProductLikeCreateOrDeleteDTO>("error user not found");

                var ifProductExist = _productService.GetProductIfExist(productIdString);

                if (ifProductExist == null)
                    return ResultService.Fail<UserProductLikeCreateOrDeleteDTO>("error user not found");

                var productId = Guid.Parse(productIdString);
                var userId = Guid.Parse(userIdString);
                Guid id = Guid.NewGuid();

                var userProductLikeIfExist = await _userProductLikeRepository.GetUserProductLikeByProductIdFullProp(productId, userId);

                if (userProductLikeIfExist != null)
                {
                    await _userProductLikeRepository.DeleteAsync(userProductLikeIfExist);

                    await _unitOfWork.Commit();

                    return ResultService.Ok(new UserProductLikeCreateOrDeleteDTO(true, false));
                }

                var userProductLike = new UserProductLike(id, productId, null, userId, null, DateTime.UtcNow);

                var userProductLikeCreate = await _userProductLikeRepository.CreateAsync(userProductLike);

                if (userProductLikeCreate == null)
                    return ResultService.Fail<UserProductLikeCreateOrDeleteDTO>("error when create UserProductLike null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<UserProductLikeCreateOrDeleteDTO>(new UserProductLikeCreateOrDeleteDTO(false, true)));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserProductLikeCreateOrDeleteDTO>(ex.Message);
            }
        }
    }
}
