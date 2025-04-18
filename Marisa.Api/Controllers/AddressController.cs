
using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AddressController(IAddressService addressService, IBaseController baseController, ICurrentUser currentUser, 
            IUserAuthenticationService userAuthenticationService)
        {
            _addressService = addressService;
            _baseController = baseController;
            _currentUser = currentUser;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpGet("v1/public/address/get-by-id-address/{addressId}")]
        public async Task<IActionResult> GetAddressDTOById([FromRoute] string addressId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _addressService.GetAddressDTOById(Guid.Parse(addressId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/address/create")]
        public async Task<IActionResult> ConfirmEmailSendCode([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.Create(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/address/send-code-email")]
        public IActionResult ConfirmEmailSendCode([FromBody] UserDTO userDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = _userAuthenticationService.SendCodeEmail(userDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/address/verify-code-send-to-email")]
        public async Task<IActionResult> Verfic([FromBody] UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var results = await _userAuthenticationService.VerifyCodeEmail(userConfirmCodeEmailDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }
    }
}
