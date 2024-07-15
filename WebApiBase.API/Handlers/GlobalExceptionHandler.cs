using Microsoft.AspNetCore.Diagnostics;
using WebApiBase.Exceptions;
using WebApiBase.Models;

namespace WebApiBase.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);
        var response = new ServiceResponse<Object>
        {
            Data = null,
            Success = false
        };

        if (exception is WebApiBaseException webApiBaseException)
        {
            response.Message = exception.Message;
            httpContext.Response.StatusCode = webApiBaseException.StatusCode;
        }
        else
        {
            response.Message = "Internal Server Error";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}