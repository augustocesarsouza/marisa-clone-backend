
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

        [HttpGet("v1/address/get-all-addresses-by-user-id/{userId}")]
        public async Task<IActionResult> GetAllAddressByUserId([FromRoute] string userId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.GetAllAddressByUserId(Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/address/create")]
        public async Task<IActionResult> Create([FromBody] AddressDTO addressDTO)
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
        public async Task<IActionResult> VerifyCodeEmailToAddNewAddress([FromBody] UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var results = await _addressService.VerifyCodeEmailToAddNewAddress(userConfirmCodeEmailDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpPut("v1/address/update")]
        public async Task<IActionResult> Update([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.Update(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/address/update-set-up-as-primary-address")]
        public async Task<IActionResult> UpdateAddressPrimary([FromBody] AddressDTO addressDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _addressService.UpdateAddressPrimary(addressDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/address/delete/{addressId}")]
        public async Task<IActionResult> Delete([FromRoute] string addressId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var results = await _addressService.DeleteAddress(Guid.Parse(addressId));

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }
    }
}
