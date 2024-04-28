using AutoMapper;
using BlogApp.Business.DTO.Comments.Requests;
using BlogApp.DataModel;
using BlogApp.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Business.Services.Comments;

public sealed class CommentService : BaseService, ICommentService
{
    public CommentService(BlogAppDbContext context,
        IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task AddCommentAsync(AddCommentRequestDto dto)
    {
        var blogPost = await Context.BlogPosts
            .AsNoTracking()
            .FirstOrDefaultAsync(bp => bp.Id == dto.BlogPostId);
        
        _ = blogPost ?? throw new ArgumentException("No BlogPosts found with specified Id");

        Context.Comments.Add(new Comment()
        {
            BlogPostId = dto.BlogPostId,
            Text = dto.Text
        });

        await Context.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(UpdateCommentRequestDto dto)
    {
        var comment = await Context.Comments
            .FirstOrDefaultAsync(bp => bp.Id == dto.Id);

        _ = comment ?? throw new ArgumentException("No Comments found with specified Id");

        comment.Text = dto.Text;

        Context.Comments.Update(comment);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteCommentByIdAsync(Guid id)
    {
        var affectedRows = await Context.Comments
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0) throw new ArgumentException("No Comments found with specified Id");
    }
}