using WebApplication1.DTOs;

namespace WebApplication1
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto?> LoginAsync(AuthRequestDto dto);
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
    }
}
