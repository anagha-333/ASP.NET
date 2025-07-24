using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RevvApplication.Controllers
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
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _tokenGenerator.GenerateToken(user.Username, user.Role);
            return Ok(new { Token = token, user.Username, user.Role });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existing = await _userService.GetByUsernameAsync(user.Username);
            if (existing != null) return BadRequest("User already exists");

            await _userService.CreateAsync(user);
            return Ok("User registered");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var username = User.Identity.Name;
            var user = await _userService.GetByUsernameAsync(username);
            return user != null ? Ok(new { user.Username, user.Role }) : NotFound();
        }
    }
}
