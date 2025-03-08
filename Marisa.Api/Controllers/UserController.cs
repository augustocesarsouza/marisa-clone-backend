using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserManagementService userManagementService, IUserAuthenticationService userAuthenticationService, 
            IBaseController baseController, ICurrentUser currentUser)
        {
            _userManagementService = userManagementService;
            _userAuthenticationService = userAuthenticationService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/user/get-by-id-info-user/{userId}")]
        public async Task<IActionResult> GetByIdInfoUser([FromRoute] string userId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userAuthenticationService.GetByIdInfoUser(userId);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/user/login/{email}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
        {
            var result = await _userAuthenticationService.Login(email, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/user/verify-password/{email}/{password}")]
        public async Task<IActionResult> VerifyPasswordUser([FromRoute] string email, [FromRoute] string password)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userAuthenticationService.VerifyPasswordUser(email, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/user/send-code-email")]
        public IActionResult ConfirmEmailSendCode([FromBody] UserDTO userDTO)
        {
            var result = _userAuthenticationService.SendCodeEmail(userDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/user/verify-code-send-to-email")]
        public async Task<IActionResult> Verfic([FromBody] UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            var results = await _userAuthenticationService.VerifyCodeEmail(userConfirmCodeEmailDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpPost("v1/public/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDTO userDTO)
        {
            var result = await _userManagementService.Create(userDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
