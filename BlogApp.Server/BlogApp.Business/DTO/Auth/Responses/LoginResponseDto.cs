namespace BlogApp.Business.DTO.Auth.Responses;

public sealed class LoginResponseDto
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
}