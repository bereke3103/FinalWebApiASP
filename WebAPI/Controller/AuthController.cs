using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models;
using WebAPI.Service.Interface;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;

        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpPost("/register")]

        public async Task<ActionResult<string>> Register(UserRegisterModel user)
        {
            
            
            var response = await auth.Register(user);

            if (response == null)
            {
                throw new Exception("Произошла ошибка");
            }

            return Ok("Регистрация прошла успешна");
        }


        [HttpPost("/authentication")]

        public async Task<ActionResult<string>> Authentication(UserAuthenModel user)
        {
            var response = await auth.Authentication(user);

            if (response == null)
            {
                throw new Exception("Произошла ошибка");
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
