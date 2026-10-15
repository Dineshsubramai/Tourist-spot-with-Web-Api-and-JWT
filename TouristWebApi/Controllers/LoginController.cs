using Microsoft.AspNetCore.Mvc;
using TouristWebApi.DTO;
using TouristWebApi.Models;
using TouristWebApi.Services;
namespace TouristWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginService loginService;
        public LoginController(ILoginService _loginService)
        {
            loginService = _loginService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<IEnumerable<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO tourDTO)
        {
            var res = await loginService.PerformLogin(tourDTO.user, tourDTO.password);
            return ok(res);
            //if (res.Length > )
            //    return Ok("res");
            //else
            //    return BadRequest("Login failed");
        }
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<string>> CreateAccount([FromBody] LoginRequestDTO tourDTO)
        {
            return BadRequest("not implemented yet");
        }
    }
}