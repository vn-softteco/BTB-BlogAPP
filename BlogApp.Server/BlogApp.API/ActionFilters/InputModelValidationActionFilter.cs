using BlogApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.API.ActionFilters;

public sealed class InputModelValidationActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        if (!actionContext.ModelState.IsValid)
        {
            var response = new ApiResponse<object>();

            var errors = actionContext.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            response.Success = false;
            response.ErrorMessage = string.Join("; ", errors);
            actionContext.Result = new BadRequestObjectResult(errors);
        }
    }
}