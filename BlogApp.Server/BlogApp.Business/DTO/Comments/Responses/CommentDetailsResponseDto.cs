namespace BlogApp.Business.DTO.Comments.Responses;

public sealed class CommentDetailsResponseDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
}