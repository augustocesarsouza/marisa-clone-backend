using Marisa.Application.DTOs;
using Marisa.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marisa.Api.ControllersInterface
{
    public interface IBaseController
    {
        public UserAuthDTO? Validator(ICurrentUser? currentUser);
        public IActionResult Forbidden();
    }
}
