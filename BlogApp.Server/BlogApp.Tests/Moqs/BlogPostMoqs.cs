using BlogApp.API.Models;
using BlogApp.Business.DTO.BlogPosts.Responses;
using BlogApp.Business.DTO.Comments.Responses;

namespace BlogApp.Tests.Moqs;

public sealed class BlogPostMoqs
{
    public const string LoremIpsumText = "Lorem Ipsum is simply dummy text...";
    
    public static List<GetBlogPostListItemResponseDto> GetAllPostsPositive()
    {
        return new List<GetBlogPostListItemResponseDto>
        {
            new GetBlogPostListItemResponseDto
            {
                Id = new Guid("9914C65F-37B9-4547-8BB1-08DC66D1643A"),
                Title= "Test Title 1",
                Content= LoremIpsumText,
                CreationDate = DateTimeOffset.Parse("2024-04-27 15:47:59.8873079 +00:00"),
                CreatedByFullName = "Vladimir Nalivaiko",
            },
            new GetBlogPostListItemResponseDto
            {
                Id = new Guid("FF14DB8B-F6CC-4B26-932E-08DC68607AE4"),
                Title= "Test Title 2",
                Content= LoremIpsumText,
                CreationDate = DateTimeOffset.Parse("2024-04-28 15:47:59.8873079 +00:00"),
                CreatedByFullName = "Vladimir Nalivaiko",
            },
        };
    }

	public static GetBlogPostDetailsResponseDto ExistingBlogPost()
	{
        return new GetBlogPostDetailsResponseDto
		{
            Id = new Guid("9914C65F-37B9-4547-8BB1-08DC66D1643A"),
            Title = "Test Title 1",
            Content = LoremIpsumText,
            CreationDate = DateTimeOffset.Parse("2024-04-27 15:47:59.8873079 +00:00"),
            CreatedByFullName = "Vladimir Nalivaiko",
            Comments = new List<CommentDetailsResponseDto>()
		};
	}

	public static GetBlogPostDetailsResponseDto ExistingBlogPost_Updated()
	{
		return new GetBlogPostDetailsResponseDto
		{
			Id = new Guid("9914C65F-37B9-4547-8BB1-08DC66D1643A"),
			Title = "UPDATED Test Title 1",
			Content = "UPDATED" + LoremIpsumText,
			CreationDate = DateTimeOffset.Parse("2024-04-27 15:47:59.8873079 +00:00"),
			CreatedByFullName = "Vladimir Nalivaiko",
			Comments = new List<CommentDetailsResponseDto>()
		};
	}
}