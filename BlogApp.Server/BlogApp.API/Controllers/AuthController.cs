using System.Net;
using AutoMapper;
using BlogApp.API.ActionFilters;
using BlogApp.API.Models;
using BlogApp.API.Models.Auth.Requests;
using BlogApp.API.Models.Auth.Responses;
using BlogApp.Business.DTO.Auth.Requests;
using BlogApp.Business.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApp.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    
    public AuthController(IAuthService authService,
        IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "User Exist, Password not following the rules or incorrect")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
    [HttpPost("register")]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        ApiResponse<string> response = new ApiResponse<string>();

        var dto = _mapper.Map<RegisterRequestDto>(model);
        var result = await _authService.Register(dto);

        if (result.Succeeded)
        {
            response.Success = true;
            return Ok(response);
        }

        response.Success = false;
        response.ErrorMessage = string.Join(",", result.Errors.Select(err => err.Description));
        return BadRequest(response);
    }
    
    /// <summary>
    /// Generates JWT token for user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoginResponse>))]
    [HttpPost("login")]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        ApiResponse<LoginResponse> response = new ApiResponse<LoginResponse>();
        var dto = _mapper.Map<LoginRequestDto>(request);
        var result = await _authService.LoginUserAsync(dto);
        response.Data = _mapper.Map<LoginResponse>(result);
        response.Success = true;
        return Ok(response);
    }
}