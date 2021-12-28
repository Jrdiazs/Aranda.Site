using Aranda.Services;
using Aranda.Services.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aranda.Site.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        private readonly IRolServices _services;

        public RolController(IRolServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new RolResponseList();
            try
            {
                response = _services.GetRolesAll();
            }
            catch (Exception ex)
            {
                response.Error(ex);
            }
            return Json(response);
        }
    }
}