using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BlogApp.DataModel.Helpers;

// TODO: move realization to API level so BAL will not depend on IHttpContextAccessor
public sealed class UserInfoProvider : IUserInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInfoProvider(IHttpContextAccessor _httpContextAccessor)
    {
        this._httpContextAccessor = _httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId?.Value is not null)
                {
                    return Guid.Parse(userId.Value);
                }
            }

            return Guid.Empty;
        }
    }
}