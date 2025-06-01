using Backend.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Services.Interfaces;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            var token = await _authService.AuthenticateAsync(request);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result)
                return BadRequest("Username already taken.");

            return Ok("User registered successfully.");
        }
    }
}
