using Marisa.Api.ControllersInterface;
using Marisa.Application.DTOs;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.Controllers
{
    [ApiController]
    public class BaseController : IBaseController
    {
        [NonAction]
        public UserAuthDTO? Validator(ICurrentUser? currentUser)
        {
            // primeiro ele vai no "ICurrentUser" e depois aqui aparentemente
            if (currentUser != null && currentUser.IsValid == false)
                return null;

            if (currentUser == null || string.IsNullOrEmpty(currentUser.Email))
                return null;

            if (!currentUser.IsValid)
                return null;

            if (!string.IsNullOrEmpty(currentUser.Email))
            {
                return new UserAuthDTO { Email = currentUser.Email };
            }

            return null;
        }

        [NonAction]
        public IActionResult Forbidden()
        {
            var obj = new
            {
                code = "acesso_negado",
                message = "Usuario não contem as devidas informações necessarias para acesso"
            };

            return new ObjectResult(obj) { StatusCode = 403 };
        }
    }
}
