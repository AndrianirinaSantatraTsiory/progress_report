using Microsoft.AspNetCore.Mvc;
using ProgressReport.DTO;
using ProgressReport.Services;

namespace ProgressReport.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<AuthResponseDto>>> Login([FromBody] LoginDto dto)
        {
            var authResponse = await _authService.Login(dto);

            var response = new ResponseModel<AuthResponseDto>("Connexion réussie", authResponse);
            return Ok(response);
        }
    }
}