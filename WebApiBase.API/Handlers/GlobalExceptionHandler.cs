using Microsoft.AspNetCore.Diagnostics;
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
            Message = exception.Message,
            Success = false
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}