namespace BlogApp.API.Models;

public sealed class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}