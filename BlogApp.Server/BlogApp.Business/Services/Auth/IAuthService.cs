using BlogApp.Business.DTO.Auth.Requests;
using BlogApp.Business.DTO.Auth.Responses;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Services.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginUserAsync(LoginRequestDto request);
    Task<IdentityResult> RegisterAsync(RegisterRequestDto dto);
}