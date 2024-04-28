namespace BlogApp.Business.DTO.Auth.Requests;

public sealed class RegisterRequestDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}