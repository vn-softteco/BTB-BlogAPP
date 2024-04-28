namespace BlogApp.API.Models.Auth.Responses;

public sealed class LoginResponse
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
}