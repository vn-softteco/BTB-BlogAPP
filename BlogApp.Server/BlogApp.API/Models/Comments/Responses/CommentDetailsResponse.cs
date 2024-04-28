namespace BlogApp.API.Models.Comments.Responses;

public sealed class CommentDetailsResponse
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
}