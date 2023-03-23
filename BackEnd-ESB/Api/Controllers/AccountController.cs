using ESB.application.DTOs.Security;
using ESB.application.Interfaces.Services.Security;
using ESB.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ESB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DoLogInAsync([FromBody] AuthenticationRequest obj)
        {
            return Ok(await _accountService.AuthenticateAsync(obj, GenerateIPAddress()));
        }

        //[AllowAnonymous]
        //[HttpPost("DoLogInCookie")]
        //[ProducesResponseType(typeof(Response<string>), 200)]
        //public async Task<IActionResult> DoLogInCookieAsync([FromBody] AuthenticationRequest obj)
        //{
        //    var AuthRespose = await _accountService.AuthenticateAsync(obj, GenerateIPAddress());
        //    if (AuthRespose.Succeeded)
        //    {
        //        var cookie = new CookieOptions();
        //        cookie.Expires = AuthRespose.Data.Expiration;
        //        var CookieName = _configuration["JWTSettings:CookieName"];
        //        Response.Cookies.Append(CookieName, AuthRespose.Data.JWToken, cookie);
        //    }

        //    return Ok(AuthRespose);
        //}

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
