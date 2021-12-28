using Aranda.Services;
using Aranda.Services.Request;
using Aranda.Site.Models;
using Aranda.Tools.String;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Site.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAppServices _services;

        public LoginController(IUserAppServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            return View(new Login() { });
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Index(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _services.Login(new UserLogin()
                    {
                        Password = login.Password,
                        UserName = login.UserName
                    });

                    if (response.Success)
                    {
                        var secretKey = StringUtil.ReadKey("Secret");
                        var issuer = StringUtil.ReadKey("Issuer");

                        var user = response.Data;

                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.HomePhone, user.Phone));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Role, user.RolName));
                        identity.AddClaim(new Claim("Id", user.Id.ToString()));


                        var claims = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);



                        var userSesion = new UserSession()
                        {
                            FullName = user.FullName,
                            Id = user.Id,
                            UserName = user.UserName
                        };

                        HttpContext.Session.SetString("UserSession", StringUtil.SerializeObject(userSesion));

                        return RedirectToAction("Index", "Home");
                    }
                    else login.Message = response.Message;
                }
            }
            catch (Exception ex)
            {
                login.Message = ex.Message;
            }

            return View(login);
        }
    }
}