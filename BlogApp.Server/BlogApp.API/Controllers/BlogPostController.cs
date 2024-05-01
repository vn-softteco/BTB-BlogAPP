using System.Net;
using AutoMapper;
using BlogApp.API.ActionFilters;
using BlogApp.API.Models;
using BlogApp.API.Models.BlogPosts.Requests;
using BlogApp.API.Models.BlogPosts.Response;
using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.Services.BlogPosts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApp.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public sealed class BlogPostController : ControllerBase
{
    private readonly IBlogPostService _blogPostService;
    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostService blogPostService,
        IMapper mapper)
    {
        _blogPostService = blogPostService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Creates Blog Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpPost]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequest request)
    {
        var response = new ApiResponse<object>();

        var dto = _mapper.Map<CreateBlogPostRequestDto>(request);
        await _blogPostService.CreateBlogPostAsync(dto);

        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <returns>List of Blog Posts</returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<GetBlogPostListItemResponse>>))]
    [HttpGet]
    public async Task<IActionResult> GetBlogPostList()
    {
        var response = new ApiResponse<List<GetBlogPostListItemResponse>>();
        
        var dto = await _blogPostService.GetBlogPostsAsync();
        
        response.Data = _mapper.Map<List<GetBlogPostListItemResponse>>(dto);
        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <returns>Blog Post with comments</returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<GetBlogPostDetailsResponse>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogPost([FromRoute] Guid id)
    {
        var response = new ApiResponse<GetBlogPostDetailsResponse>();
        
        var dto = await _blogPostService.GetBlogPostByIdAsync(id);
        
        response.Data = _mapper.Map<GetBlogPostDetailsResponse>(dto);
        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Updates Blog Post
    /// </summary>
    /// <param name="request"></param>
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpPut]
    [InputModelValidationActionFilter]
    public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostRequest request)
    {
        var response = new ApiResponse<object>();

        var dto = _mapper.Map<UpdateBlogPostRequestDto>(request);
        await _blogPostService.UpdateBlogPostAsync(dto);

        response.Success = true;
        return Ok(response);
    }
    
    /// <summary>
    /// Deletes Blog Post
    /// </summary>
    /// <returns>Empty result</returns>
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
    {
        var response = new ApiResponse<object>();

        await _blogPostService.DeleteBlogPostByIdAsync(id);

        response.Success = true;
        return Ok(response);
    }
}