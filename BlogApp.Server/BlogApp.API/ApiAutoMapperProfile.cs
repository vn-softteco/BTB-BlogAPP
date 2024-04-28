using AutoMapper;
using BlogApp.API.Models.Auth.Requests;
using BlogApp.API.Models.Auth.Responses;
using BlogApp.API.Models.BlogPosts.Requests;
using BlogApp.API.Models.BlogPosts.Response;
using BlogApp.API.Models.Comments.Requests;
using BlogApp.API.Models.Comments.Responses;
using BlogApp.Business.DTO.Auth.Requests;
using BlogApp.Business.DTO.Auth.Responses;
using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.DTO.BlogPosts.Responses;
using BlogApp.Business.DTO.Comments.Requests;
using BlogApp.Business.DTO.Comments.Responses;

namespace BlogApp.API;

public sealed class ApiAutoMapperProfile : Profile
{
    public ApiAutoMapperProfile()
    {
        CreateMap<LoginRequest, LoginRequestDto>();
        CreateMap<LoginResponseDto, LoginResponse>();
        
        CreateMap<RegisterRequest, RegisterRequestDto>();
        
        CreateMap<CreateBlogPostRequest, CreateBlogPostRequestDto>();
        CreateMap<UpdateBlogPostRequest, UpdateBlogPostRequestDto>();
        CreateMap<GetBlogPostDetailsResponseDto, GetBlogPostDetailsResponse>();
        CreateMap<GetBlogPostListItemResponseDto, GetBlogPostListItemResponse>();
        
        CreateMap<CommentDetailsResponseDto, CommentDetailsResponse>();
        CreateMap<AddCommentRequest, AddCommentRequestDto>();
        CreateMap<UpdateCommentRequest, UpdateCommentRequestDto>();
    }
}