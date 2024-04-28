namespace BlogApp.Business.DTO.BlogPosts.Responses;

public sealed class GetBlogPostListItemResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
}