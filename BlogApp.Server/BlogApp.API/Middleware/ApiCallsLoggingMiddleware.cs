using Microsoft.AspNetCore.Http.Extensions;

namespace BlogApp.API.Middleware;

public sealed class ApiCallsLoggingMiddleware: IMiddleware
{
    private const string LogDateFormat = "dd/MM/yy HH:mm:ss";
    
    private readonly ILogger<ApiCallsLoggingMiddleware> _logger;
    
    public ApiCallsLoggingMiddleware(ILogger<ApiCallsLoggingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.EnableBuffering();
        LogRequest(context);
        await next(context);
        LogResponse(context);
    }
    
    private void LogRequest(HttpContext context)
    {
        _logger.LogInformation("[{Time}] [{TraceIdentifier}] {Method} {Url}",
            DateTime.UtcNow.ToString(LogDateFormat),
            context.TraceIdentifier,
            context.Request.Method,
            context.Request.GetDisplayUrl());
    }

    private void LogResponse(HttpContext context)
    {
        _logger.LogInformation("[{Time}] [{TraceIdentifier}] {StatusCode}",
            DateTime.UtcNow.ToString(LogDateFormat),
            context.TraceIdentifier,
            context.Response.StatusCode);
    }
}