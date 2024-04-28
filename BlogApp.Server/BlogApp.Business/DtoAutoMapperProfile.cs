using AutoMapper;
using BlogApp.Business.DTO.Auth.Requests;
using BlogApp.Business.DTO.BlogPosts.Requests;
using BlogApp.Business.DTO.BlogPosts.Responses;
using BlogApp.Business.DTO.Comments.Requests;
using BlogApp.Business.DTO.Comments.Responses;
using BlogApp.DataModel.Entities;

namespace BlogApp.Business;

public sealed class DtoAutoMapperProfile : Profile
{
    public DtoAutoMapperProfile()
    {
        CreateMap<RegisterRequestDto, User>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(s => Guid.NewGuid().ToString()));
        CreateMap<CreateBlogPostRequestDto, BlogPost>();
        CreateMap<AddCommentRequestDto, Comment>();
        CreateMap<BlogPost, GetBlogPostDetailsResponseDto>()
            .ForMember(d => d.CreatedByFullName, opt => opt.MapFrom(s => s.CreatedBy.FullName));
        CreateMap<BlogPost, GetBlogPostListItemResponseDto>()
            .ForMember(d => d.CreatedByFullName, opt => opt.MapFrom(s => s.CreatedBy.FullName));
        CreateMap<Comment, CommentDetailsResponseDto>()
            .ForMember(d => d.CreatedByFullName, opt => opt.MapFrom(s => s.CreatedBy.FullName));
    }
}