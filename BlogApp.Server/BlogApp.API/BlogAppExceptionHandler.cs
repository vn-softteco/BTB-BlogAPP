using System.Net;
using BlogApp.API.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace BlogApp.API
{
	public sealed class BlogAppExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
			Exception exception,
			CancellationToken cancellationToken)
		{
			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var response = new ApiResponse<object>();
			response.ErrorMessage = exception.Message;
			await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
			return true;
		}
	}
}
