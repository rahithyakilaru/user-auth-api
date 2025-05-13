using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User_Authentication_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auth;
        public AuthenticationController(IAuthentication auth)
        {
            _auth = auth;
        }
        [HttpPost("{signup}")]
        public IActionResult SignUp([FromBody] UserModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok("User registered successfully!");
        }
        [HttpPost("{login}")]
        public IActionResult Login([FromBody] UserModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_auth.ValidateUser(user.Username, user.Password))
            {
                var token = _auth.GenerateJwtToken(user.Username);
                return Ok(new { token });
            }
            return Unauthorized("Invalid username or password");
        }
        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedRoute()
        {
            return Ok("You don't have accessed a protected route!");
        }
    }
}
