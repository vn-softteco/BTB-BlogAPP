using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Comments.Requests;

public sealed class UpdateCommentRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Text is required")]
    [MaxLength(length: 1000, ErrorMessage = "Text length has to be less than 10000")]
    public string Text { get; set; }
}