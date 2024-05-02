using AutoMapper;
using BlogApp.API.Controllers;
using BlogApp.API;
using Moq;
using BlogApp.Business.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using BlogApp.API.Models.Auth.Requests;
using BlogApp.Tests.Moqs;
using BlogApp.API.Models;
using BlogApp.API.Models.Auth.Responses;
using BlogApp.Business.DTO.Auth.Responses;
using BlogApp.Business.DTO.Auth.Requests;
using Microsoft.AspNetCore.Identity;
using BlogApp.Common.Exceptions;

namespace BlogApp.Tests.Tests;

public sealed class AuthControllerTests
{
	private readonly AuthController _authController;
	private readonly Mock<IAuthService> _authService;
	private readonly Mock<IMapper> _mapper;

	public AuthControllerTests()
	{
		_mapper = new Mock<IMapper>();
		_authService = new Mock<IAuthService>();
		_authController = new AuthController(_authService.Object, _mapper.Object);
	}

	[Fact]
	public async Task LoginUser_Success()
	{
		var request = new LoginRequest()
		{
			Email = AuthMoqs.Email,
			Password = AuthMoqs.Password
		};

		var requestDto = new LoginRequestDto()
		{
			Email = AuthMoqs.Email,
			Password = AuthMoqs.Password
		};

		var responseDto = new LoginResponseDto()
		{
			Token = AuthMoqs.SuccessLoginResultDto.Token,
			UserId = AuthMoqs.SuccessLoginResultDto.UserId
		};

		//Arrange
		_mapper.Setup(m => m.Map<LoginRequestDto>(request)).Returns(requestDto);
		_mapper.Setup(m => m.Map<LoginResponse>(AuthMoqs.SuccessLoginResultDto)).Returns(AuthMoqs.SuccessLoginResult);
		_authService.Setup(service => service.LoginUserAsync(requestDto)).ReturnsAsync(AuthMoqs.SuccessLoginResultDto);

		//Act
		var result = await _authController.LoginAsync(request);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<LoginResponse>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var loginResult = value as ApiResponse<LoginResponse>;
		Assert.NotNull(loginResult);
		Assert.NotNull(loginResult.Data);
		Assert.NotNull(loginResult.Data.Token);
	}

	[Fact]
	public async Task RegisterUser_Success()
	{
		var request = new RegisterRequest()
		{
			Email = AuthMoqs.Email,
			Password = AuthMoqs.Password,
			FirstName = AuthMoqs.FirstName,
			LastName = AuthMoqs.LastName,
		};

		var requestDto = new RegisterRequestDto()
		{
			Email = AuthMoqs.Email,
			Password = AuthMoqs.Password,
			FirstName = AuthMoqs.FirstName,
			LastName = AuthMoqs.LastName,
		};

		//Arrange
		_mapper.Setup(m => m.Map<RegisterRequestDto>(request)).Returns(requestDto);
		_authService.Setup(service => service.RegisterAsync(requestDto)).ReturnsAsync(IdentityResult.Success);

		//Act
		var result = await _authController.Register(request);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		Assert.Equal(200, okResult.StatusCode);
	}
}