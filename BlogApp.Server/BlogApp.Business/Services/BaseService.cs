using AutoMapper;
using BlogApp.DataModel;

namespace BlogApp.Business.Services;

public abstract class BaseService
{
    protected readonly BlogAppDbContext Context;
    protected readonly IMapper Mapper;

    protected BaseService(BlogAppDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
}