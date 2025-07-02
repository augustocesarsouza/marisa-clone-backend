using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class ProductCommentController : ControllerBase
    {
        private readonly IProductCommentService _productCommentService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductCommentController(IProductCommentService productCommentService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productCommentService = productCommentService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/product-comment/get-product-comment-by-id/{productCommentId}")]
        public async Task<IActionResult> GetProductCommentById([FromRoute] string productCommentId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentService.GetProductCommentById(Guid.Parse(productCommentId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/product-comment/get-all-product-comment-by-productid/{productId}")]
        public async Task<IActionResult> GetAllProductCommentByUserIdAndProductId([FromRoute] string productId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentService.GetAllProductCommentByUserIdAndProductId(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/product-comment/create")]
        public async Task<IActionResult> Create([FromBody] ProductCommentCreate productCommentCreate)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productCommentService.Create(productCommentCreate);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/product-comment/delete/{productCommentId}")]
        public async Task<IActionResult> Delete([FromRoute] string productCommentId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var results = await _productCommentService.Delete(Guid.Parse(productCommentId));

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }
    }
}
