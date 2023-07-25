using Microsoft.AspNetCore.Mvc;
using TaskSystem.Application.Services;
using TaskSystem.Domain.Models;

namespace TaskSystem.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if(username == "emerson" && password == "123123123")
            {
                // var token = TokenService.GenerateToken(new Employee());
                var token = "odsjkcojapfoijiepwjfpiw";
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}