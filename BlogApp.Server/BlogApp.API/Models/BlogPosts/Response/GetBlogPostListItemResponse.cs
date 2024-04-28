namespace BlogApp.API.Models.BlogPosts.Response;

public sealed class GetBlogPostListItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
}