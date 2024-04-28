using BlogApp.API.Models.Comments.Responses;

namespace BlogApp.API.Models.BlogPosts.Response;

public sealed class GetBlogPostDetailsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
    
    public List<CommentDetailsResponse> Comments { get; set; }
}