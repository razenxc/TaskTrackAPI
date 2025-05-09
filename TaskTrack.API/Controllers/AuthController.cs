using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTrack.API.Contracts;
using TaskTrack.Application.Services;
using TaskTrack.Domain.Entities;

namespace TaskTrack.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] UserRegisterRequest request)
        {
            User user = await _authService.Register(request.Username, request.Email, request.Password);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(new UserResponse(user.Id, user.Username, user.Email, user.Role));
        }

        [HttpPost("login")]
        public async Task<ActionResult<JwtTokens>> Login([FromBody] UserLoginRequest request)
        {
            JwtTokens jwtTokens = await _authService.Login(request.Username, request.Password);
            if(jwtTokens == null)
            {
                return BadRequest();
            }

            return Ok(jwtTokens);
        }

        [HttpPost("refreshTokens")]
        public async Task<ActionResult<JwtTokens>> RefreshTokens([FromBody] RefreshTokensRequest request)
        {
            JwtTokens jwtTokens = await _authService.RefreshTokens(request.refreshToken);
            if (jwtTokens == null)
            {
                return BadRequest();
            }

            return Ok(jwtTokens);
        }
    }
}
