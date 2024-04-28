using BlogApp.Business.DTO.Comments.Requests;

namespace BlogApp.Business.Services.Comments;

public interface ICommentService
{
    public Task AddCommentAsync(AddCommentRequestDto dto);
    public Task UpdateCommentAsync(UpdateCommentRequestDto dto);
    public Task DeleteCommentByIdAsync(Guid id);
}