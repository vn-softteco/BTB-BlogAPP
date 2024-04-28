using AutoMapper;
using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.DTO.BlogPosts.Responses;
using BlogApp.DataModel;
using BlogApp.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Business.Services.BlogPosts;

public sealed class BlogPostService : BaseService, IBlogPostService
{
    public BlogPostService(BlogAppDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    public async Task CreateBlogPostAsync(CreateBlogPostRequestDto dto)
    {
        var blogPost = Mapper.Map<BlogPost>(dto);
        await Context.BlogPosts.AddAsync(blogPost);
        await Context.SaveChangesAsync();
    }

    public async Task<List<GetBlogPostListItemResponseDto>> GetBlogPostsAsync()
    {
        var blogPosts = await Context.BlogPosts
            .AsNoTracking()
            .Include(bp => bp.CreatedBy)
            .ToListAsync();

        return Mapper.Map<List<GetBlogPostListItemResponseDto>>(blogPosts);
    }

    public async Task<GetBlogPostDetailsResponseDto> GetBlogPostByIdAsync(Guid id)
    {
        var blogPost = await Context.BlogPosts
            .AsNoTracking()
            .Include(bp => bp.CreatedBy)
            .Include(bp => bp.Comments)
            .ThenInclude(bp => bp.CreatedBy)
            .FirstOrDefaultAsync(bp => bp.Id == id);
        
        _ = blogPost ?? throw new ArgumentException("No BlogPosts found with specified Id");

        return Mapper.Map<GetBlogPostDetailsResponseDto>(blogPost);
    }

    public async Task UpdateBlogPostAsync(UpdateBlogPostRequestDto dto)
    {
        var blogPost = await Context.BlogPosts
            .FirstOrDefaultAsync(bp => bp.Id == dto.Id);

        _ = blogPost ?? throw new ArgumentException("No BlogPosts found with specified Id");

        blogPost.Title = dto.Title;
        blogPost.Content = dto.Content;

        Context.BlogPosts.Update(blogPost);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteBlogPostByIdAsync(Guid id)
    {
        var affectedRows = await Context.BlogPosts
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0) throw new ArgumentException("No BlogPosts found with specified Id");
    }
}