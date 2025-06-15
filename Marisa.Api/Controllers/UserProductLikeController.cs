using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class UserProductLikeController : ControllerBase
    {
        private readonly IUserProductLikeService _userProductLikeService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserProductLikeController(IUserProductLikeService userProductLikeService, IBaseController baseController, ICurrentUser currentUser)
        {
            _userProductLikeService = userProductLikeService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/user-product-like/get-user-product-by-id/{userProductLikeId}")]
        public async Task<IActionResult> GetUserProductLikeById([FromRoute] string userProductLikeId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userProductLikeService.GetUserProductLikeById(Guid.Parse(userProductLikeId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/user-product-like/get-user-product-by-product-id/{productId}/{userId}")]
        public async Task<IActionResult> GetUserProductLikeByProductId([FromRoute] string productId, [FromRoute] string userId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userProductLikeService.GetUserProductLikeByProductId(Guid.Parse(productId), Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/user-product-like/get-all-user-product-like")]
        public async Task<IActionResult> GetAllUserProductLike()
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userProductLikeService.GetAllUserProductLike();

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/user-product-like/create")]
        public async Task<IActionResult> Create([FromBody] UserProductLikeDTO userProductLikeDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userProductLikeService.Create(userProductLikeDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
