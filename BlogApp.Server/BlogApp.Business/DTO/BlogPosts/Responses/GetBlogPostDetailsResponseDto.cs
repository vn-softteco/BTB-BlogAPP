using BlogApp.Business.DTO.Comments.Responses;

namespace BlogApp.Business.DTO.BlogPosts.Responses;

public sealed class GetBlogPostDetailsResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string CreatedByFullName { get; set; }
    
    public List<CommentDetailsResponseDto> Comments { get; set; }
}