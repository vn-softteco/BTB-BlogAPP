namespace BlogApp.Business.DTO.BlogPosts.Requests;

public sealed class CreateBlogPostRequestDto
{
    public string Title { get; set; }
    public string Content { get; set; }
}