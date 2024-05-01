using System.Net;
using AutoMapper;
using BlogApp.API.ActionFilters;
using BlogApp.API.Models;
using BlogApp.API.Models.Comments.Requests;
using BlogApp.Business.DTO.Comments.Requests;
using BlogApp.Business.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApp.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public sealed class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService,
        IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Adds Comment to Blog Post
    /// </summary>
    /// <param name="request"></param>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpPost]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
    {
        var response = new ApiResponse<object>();
        
        var dto = _mapper.Map<AddCommentRequestDto>(request);
        await _commentService.AddCommentAsync(dto);

        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Updates Comment
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpPut]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request)
    {
        var response = new ApiResponse<object>();
        
        var dto = _mapper.Map<UpdateCommentRequestDto>(request);
        await _commentService.UpdateCommentAsync(dto);

        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Deletes comment
    /// </summary>
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
    {
        var response = new ApiResponse<object>();

        await _commentService.DeleteCommentByIdAsync(id);

        response.Success = true;
        return Ok(response);
    }
}