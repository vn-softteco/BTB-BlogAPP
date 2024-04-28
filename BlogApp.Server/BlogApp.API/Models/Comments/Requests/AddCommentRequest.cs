using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Comments.Requests;

public sealed class AddCommentRequest
{
    [Required(ErrorMessage = "BlogPostId is required")]
    public Guid BlogPostId { get; set; }
    
    [Required(ErrorMessage = "Text is required")]
    [MaxLength(length: 1000, ErrorMessage = "Text length has to be less than 10000")]
    public string Text { get; set; }
}