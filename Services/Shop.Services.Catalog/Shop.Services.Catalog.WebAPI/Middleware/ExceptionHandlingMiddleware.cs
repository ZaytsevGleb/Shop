using System.Net;
using Newtonsoft.Json;
using Shop.Services.Catalog.BusinessLogic.Common.Exceptions;
using Shop.Services.Catalog.WebAPI.Constants;
using Shop.Services.Catalog.WebAPI.Dtos;

namespace Cryptex.Services.OperationService.WebAPI.Middleware;

public sealed class CustomExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<CustomExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        string result;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = validationException.FormattedMessage });
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = badRequestException.Message });
                break;
            case ForbiddenException forbiddenException:
                code = HttpStatusCode.Forbidden;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = forbiddenException.Message });
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = notFoundException.Message });
                break;
            default:
                result = JsonConvert.SerializeObject(new ErrorDto
                    { Message = exception.Message, Description = exception.StackTrace });
                logger.LogError(exception, exception.Message);
                break;
        }

        context.Response.ContentType = ApiConstants.ContentType;
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(result);
    }
}