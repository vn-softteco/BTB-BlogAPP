using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using BlogApp.Business.DTO.Auth.Requests;
using BlogApp.Business.DTO.Auth.Responses;
using BlogApp.Business.Helpers;
using BlogApp.Common.Exceptions;
using BlogApp.DataModel;
using BlogApp.DataModel.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Services.Auth;

public class AuthService : BaseService, IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager,
        BlogAppDbContext context,
        IMapper mapper)
        : base(context, mapper)
    {
        _userManager = userManager;
    }

    public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception("User doesn't exist!");

        if (await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var token = JWTHelper.CreateNewToken(user) ?? throw new Exception("Error with token generation!");

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
            };
        }
        else
        {
            throw new BlogAppException("Incorrect password!");
        }
    }
    
    public async Task<IdentityResult> RegisterAsync(RegisterRequestDto dto)
    {
        var userSearchResult = await _userManager.FindByEmailAsync(dto.Email);
        if (userSearchResult is not null)
        {
            throw new BlogAppException("User already exists!");
        }

        User user = Mapper.Map<User>(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);

        return result;
    }
}