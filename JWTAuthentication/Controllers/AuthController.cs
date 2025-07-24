using JWTAuthentication.model;
using JWTAuthentication.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly UserService _userService;

        public AuthController(JwtTokenGenerator tokenGenerator, UserService userService)
        {
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = _tokenGenerator.GenerateToken(user.Username, user.Role); // include role in token
            return Ok(new
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existing = await _userService.GetByUsernameAsync(user.Username);
            if (existing != null)
                return BadRequest("Username already exists");

            await _userService.CreateAsync(user);
            return Ok("User registered successfully");
        }

        // 🔐 Get current user info (requires authentication)
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var username = User.Identity?.Name;
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null) return NotFound("User not found");

            return Ok(new
            {
                user.Username,
                user.Role
            });
        }

       
        [Authorize]
        [HttpPost("echo")]
        public IActionResult EchoTest([FromBody] object input)
        {
            return Ok(new
            {
                Message = "You are authenticated!",
                Input = input,
                User = User.Identity?.Name
            });
        }
    }
   
}
