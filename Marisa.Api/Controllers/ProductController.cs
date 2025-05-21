using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductController(IProductService productService, IBaseController baseController, ICurrentUser currentUser)
        {
            _productService = productService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/product/get-by-product-id/{productId}")]
        public async Task<IActionResult> GetAddressDTOById([FromRoute] string productId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productService.GetProductById(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/product/create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productService.Create(productDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/public/product/delete/{productId}")]
        public async Task<IActionResult> Delete([FromRoute] string productId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _productService.Delete(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
