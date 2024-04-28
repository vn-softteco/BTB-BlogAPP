namespace BlogApp.Business.DTO.BlogPosts.Requests;

public sealed class UpdateBlogPostRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}