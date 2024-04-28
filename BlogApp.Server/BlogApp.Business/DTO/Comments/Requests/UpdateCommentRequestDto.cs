namespace BlogApp.Business.DTO.Comments.Requests;

public sealed class UpdateCommentRequestDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}