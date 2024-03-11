using System.Diagnostics;

namespace Cryptex.Services.OperationService.WebAPI.Middleware;

public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var uri = BuildUriFromRequest(context.Request);
        var method = context.Request.Method;

        logger.LogInformation("Request {uri} ({method}) started", uri, method);

        var timer = new Stopwatch();
        timer.Start();

        try
        {
            await next(context);
        }
        finally
        {
            timer.Stop();
        }

        logger.LogInformation(
            "Request {uri} ({method}) finished ( Status code: {statusCode}, Duration: {duration} ms )",
            uri,
            method,
            context.Response.StatusCode,
            timer.ElapsedMilliseconds);
    }

    private Uri BuildUriFromRequest(HttpRequest request)
    {
        var builder = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host
        };

        if (request.Host.Port.HasValue)
        {
            builder.Port = request.Host.Port.Value;
        }

        builder.Path = request.Path.ToString();
        builder.Query = request.QueryString.ToString();

        return builder.Uri;
    }
}