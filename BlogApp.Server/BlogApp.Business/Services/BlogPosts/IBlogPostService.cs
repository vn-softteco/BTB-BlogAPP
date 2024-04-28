using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.DTO.BlogPosts.Responses;

namespace BlogApp.Business.Services.BlogPosts;

public interface IBlogPostService
{
    public Task CreateBlogPostAsync(CreateBlogPostRequestDto dto);
    public Task<List<GetBlogPostListItemResponseDto>> GetBlogPostsAsync();
    public Task<GetBlogPostDetailsResponseDto> GetBlogPostByIdAsync(Guid id);
    public Task UpdateBlogPostAsync(UpdateBlogPostRequestDto dto);
    public Task DeleteBlogPostByIdAsync(Guid id);
}