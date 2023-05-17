using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Application.Dtos;
using ItunesApi.Application.helper.IJwtServices;
using ItunesApi.Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace itunesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountsBL getLoginBL;
        private readonly IScoreBL getScoreBL;
        public AccountController(IAccountsBL getLoginBL, IScoreBL getScoreBL)
        {
            this.getLoginBL = getLoginBL;
            this.getScoreBL = getScoreBL;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Account>> RegisterAccount(AccountDto loginDto)
        {
            var registerAccount = await getLoginBL.CreateAccount(loginDto);
            return Ok(registerAccount);
        }   
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Account>> LoginAccount(AccountDto loginDto)
        {
            var jwt = await getLoginBL.GetAccountsByUserName(loginDto);
            if (jwt == "WrongEmail" || jwt == "WrongPassword")
            {
                return Unauthorized();
            }
            else
            {
                return Ok(jwt);
            }
        }


        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<Account>> User()
        {
            var id = HttpContext.User.FindFirstValue("sub");
            try
            {
                var user = await getLoginBL.GetAccountById(Convert.ToInt32(id));
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            
            return Ok(new
            {
                message = "Logout Succseful"
            });
        }
    }
}
