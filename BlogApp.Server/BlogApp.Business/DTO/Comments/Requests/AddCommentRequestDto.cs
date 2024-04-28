namespace BlogApp.Business.DTO.Comments.Requests;

public sealed class AddCommentRequestDto
{
    public Guid BlogPostId { get; set; }
    public string Text { get; set; }
}