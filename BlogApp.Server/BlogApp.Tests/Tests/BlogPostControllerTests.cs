using AutoMapper;
using BlogApp.API;
using BlogApp.API.Controllers;
using BlogApp.API.Models;
using BlogApp.API.Models.BlogPosts.Requests;
using BlogApp.API.Models.BlogPosts.Response;
using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.Services.BlogPosts;
using BlogApp.Common.Exceptions;
using BlogApp.Tests.Moqs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace BlogApp.Tests.Tests;

public sealed class BlogPostControllerTests
{
	private readonly BlogPostController _blogPostController;
	private readonly Mock<IBlogPostService> _blogPostService;
	private readonly IMapper _mapper;

	public BlogPostControllerTests()
	{
		var profile = new ApiAutoMapperProfile();
		var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
		IMapper mapper = new Mapper(configuration);
		_mapper = mapper;
		_blogPostService = new Mock<IBlogPostService>();
		_blogPostController = new BlogPostController(_blogPostService.Object, mapper);
	}

	[Fact]
	public async Task GetBlogPosts_Success()
	{
		//Arrange
		_blogPostService.Setup(service => service.GetBlogPostsAsync()).ReturnsAsync(BlogPostMoqs.GetAllPostsPositive());

		//Act
		var result = await _blogPostController.GetBlogPostList();

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<List<GetBlogPostListItemResponse>>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var resultList = value as ApiResponse<List<GetBlogPostListItemResponse>>;
		Assert.NotNull(resultList);
		Assert.NotNull(resultList.Data);
		Assert.Equal(2, resultList.Data.Count);
		Assert.True(resultList.Success);
		Assert.True(resultList.ErrorMessage.IsNullOrEmpty());
	}

	[Fact]
	public async Task GetBlogPostById_Success()
	{
		var existingBlogPostId = new Guid("9914C65F-37B9-4547-8BB1-08DC66D1643A");

		//Arrange
		_blogPostService.Setup(service => service.GetBlogPostByIdAsync(existingBlogPostId)).ReturnsAsync(BlogPostMoqs.ExistingBlogPost());

		//Act
		var result = await _blogPostController.GetBlogPost(existingBlogPostId);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<GetBlogPostDetailsResponse>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var response = value as ApiResponse<GetBlogPostDetailsResponse>;
		Assert.NotNull(response);
		Assert.NotNull(response.Data);
		Assert.Equal(existingBlogPostId, response.Data.Id);
		Assert.True(response.Success);
		Assert.True(response.ErrorMessage.IsNullOrEmpty());
	}

	[Fact]
	public async Task GetBlogPostById_Fail()
	{
		var notExistingBlogPostId = new Guid("9914C65F-1111-1111-1111-08DC66D1643A");

		//Arrange
		_blogPostService.Setup(service => service.GetBlogPostByIdAsync(notExistingBlogPostId))
			.ThrowsAsync(new BlogAppException("No BlogPosts found with specified Id"));

		//Act
		Func<Task> act = async () => await _blogPostController.GetBlogPost(notExistingBlogPostId);

		//Assert
		await Assert.ThrowsAsync<BlogAppException>(act);
	}

	[Fact]
	public async Task UpdateBlogPost_Success()
	{
		var input = new UpdateBlogPostRequest()
		{
			Id = new Guid("9914C65F-1111-1111-1111-08DC66D1643A"),
			Content = BlogPostMoqs.ExistingBlogPost().Content,
			Title = BlogPostMoqs.ExistingBlogPost().Title,
		};

		var inputDto = _mapper.Map<UpdateBlogPostRequestDto>(input);

		//Arrange
		_blogPostService.Setup(service => service.UpdateBlogPostAsync(inputDto));

		//Act
		var result = await _blogPostController.UpdateBlogPost(input);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<object>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var response = value as ApiResponse<object>;
		Assert.NotNull(response);
		Assert.True(response.Success);
	}

	[Fact]
	public async Task CreateBlogPost_Success()
	{
		var existingBlogPostId = new Guid("9914C65F-37B9-4547-8BB1-08DC66D1643A");

		//Arrange
		_blogPostService.Setup(service => service.DeleteBlogPostByIdAsync(existingBlogPostId));

		//Act
		var result = await _blogPostController.DeleteBlogPost(existingBlogPostId);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<object>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var response = value as ApiResponse<object>;
		Assert.NotNull(response);
		Assert.True(response.Success);
	}

	[Fact]
	public async Task DeleteBlogPost_Success()
	{
		var input = new CreateBlogPostRequest()
		{
			Content = BlogPostMoqs.ExistingBlogPost().Content,
			Title = BlogPostMoqs.ExistingBlogPost().Title,
		};

		var inputDto = _mapper.Map<CreateBlogPostRequestDto>(input);

		//Arrange
		_blogPostService.Setup(service => service.CreateBlogPostAsync(inputDto));

		//Act
		var result = await _blogPostController.CreateBlogPost(input);

		var okResult = result as OkObjectResult;

		//Assert
		Assert.NotNull(okResult);
		var value = okResult.Value;
		Assert.IsType<ApiResponse<object>>(value);
		Assert.Equal(200, okResult.StatusCode);
		var response = value as ApiResponse<object>;
		Assert.NotNull(response);
		Assert.True(response.Success);
	}
}