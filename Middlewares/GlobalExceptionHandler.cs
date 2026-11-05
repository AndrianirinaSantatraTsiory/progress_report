using Microsoft.AspNetCore.Diagnostics;
using ProgressReport.DTO;

namespace ProgressReport.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = exception.Message;
            if (exception is KeyNotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
            }

            ResponseModel<object> response = new(message, null);
            

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    
    }
}