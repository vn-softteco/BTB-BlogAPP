using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.BlogPosts.Requests;

public sealed class UpdateBlogPostRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    [MaxLength(length: 1000, ErrorMessage = "Content length has to be less than 10000")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    [MaxLength(length: 5000, ErrorMessage = "Content length has to be less than 5000")]
    public string Content { get; set; }
}