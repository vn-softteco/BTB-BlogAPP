using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Auth.Requests;

public sealed class LoginRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}