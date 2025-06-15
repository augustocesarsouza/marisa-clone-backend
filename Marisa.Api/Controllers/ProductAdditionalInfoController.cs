using Marisa.Api.ControllersInterface;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class ProductAdditionalInfoController : ControllerBase
    {
        private readonly IProductAdditionalInfoService productAdditionalInfoService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductAdditionalInfoController(IProductAdditionalInfoService productAdditionalInfoService, IBaseController baseController, ICurrentUser currentUser)
        {
            this.productAdditionalInfoService = productAdditionalInfoService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/public/product-additional-info/get-by-id/{id}")]
        public async Task<IActionResult> GetProductAdditionalInfoById([FromRoute] string id)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await productAdditionalInfoService.GetProductAdditionalInfoById(Guid.Parse(id));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/product-additional-info/get-by-product-id/{productId}")]
        public async Task<IActionResult> GetProductAdditionalInfoByProductId([FromRoute] string productId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await productAdditionalInfoService.GetProductAdditionalInfoByProductId(Guid.Parse(productId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
