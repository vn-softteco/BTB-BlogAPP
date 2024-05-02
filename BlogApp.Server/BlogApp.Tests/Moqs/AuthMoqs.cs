using BlogApp.API.Models.Auth.Responses;
using BlogApp.Business.DTO.Auth.Responses;
using BlogApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Tests.Moqs;

public sealed class AuthMoqs
{
	public const string AccessTokenExample = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
		"eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1l" +
		"Ijoidi5uYWxpdmFpa29Ac29mdHRlY28uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3Mv" +
		"MjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJmYTU5MmFmZi05YzFjLTQwZDgt" +
		"ZGI1YS0wOGRjNjZkMTBkY2YiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lk" +
		"ZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ2Lm5hbGl2YWlrb0Bzb2Z0dGVjby5jb20iLCJqdGki" +
		"OiIyMWJlODBhYS02NmQwLTQ0ZGUtOGI4NC0zNGM3YjJmY2YyMWMiLCJleHAiOjE3MTQ2NTU1NjAsImlz" +
		"cyI6IkJsb2dBcHBBcGlTZXJ2ZXIiLCJhdWQiOiJCbG9nQXBwQ2xpZW50In0.9EnihNugxqG9sF0GfE9S" +
		"SiGwQW2Ybxb7-Gti1DRwVXw";

	public const string UserIdExample = "9914C65F-37B9-4547-8BB1-08DC66D1643A";
	public const string Email = "blog@app.com"; 
	public const string Password = "Q1j17dn$1v"; 
	public const string FirstName = "John"; 
	public const string LastName = "Doe"; 

	public static LoginResponseDto SuccessLoginResultDto = new LoginResponseDto
	{
		Token = AccessTokenExample,
		UserId = new Guid(UserIdExample)
	};

	public static LoginResponse SuccessLoginResult = new LoginResponse
	{
		Token = AccessTokenExample,
		UserId = new Guid(UserIdExample)
	};

	public static User ValidUser()
	{
		return new User()
		{
			Id = new Guid(UserIdExample),
			Email = "v.nalivaiko@softteco.com"
		};
	}
}