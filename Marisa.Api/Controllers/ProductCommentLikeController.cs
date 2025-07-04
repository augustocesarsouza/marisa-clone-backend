using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class ProductCommentLikeController : ControllerBase
    {
        private readonly IProductCommentLikeService _productCommentLikeService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductCommentLikeController(IProductCommentLikeService productCommentLikeService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productCommentLikeService = productCommentLikeService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/product-comment-like/get-by-product-comment-like-id/{productCommentLikeId}")]
        public async Task<IActionResult> GetProductCommentLikeById([FromRoute] string productCommentLikeId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentLikeService.GetProductCommentLikeById(Guid.Parse(productCommentLikeId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/product-comment-like/get-all-product-comment-like-product-id/{productId}")]
        public async Task<IActionResult> GetAllProductCommentLikeByProductId([FromRoute] string productId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentLikeService.GetAllProductCommentLikeByProductId(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/product-comment-like/create")]
        public async Task<IActionResult> Create([FromBody] ProductCommentLikeCreate productCommentLikeCreate)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentLikeService.Create(productCommentLikeCreate);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
