using Microsoft.AspNetCore.Identity;
using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserService userService, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _userService.GetUserEntityByEmail(dto.Username) ?? throw new UnauthorizedAccessException("Identifiants invalides.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Identifiants invalides.");
            }

            var token = _tokenService.GenerateToken(user.Email, user.Role.ToString());

            return new AuthResponseDto(token, user.Email, user.Role.ToString());
        }
    }
}