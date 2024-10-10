using BusinessLayer.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        ILogger<GlobalExceptionHandler> logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) { 
        logger = _logger;
        }
      async  ValueTask<bool> IExceptionHandler.TryHandleAsync
            (HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errResp = new ErrorResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            };
            logger.LogError(string.Format("Exception occured while call URL {0} and statuscode is " +
                "{1} and the Exception is {2}"
                ,httpContext.Request.Path,httpContext.Response.StatusCode,exception.Message
            ));
            await httpContext.Response.WriteAsJsonAsync(errResp, cancellationToken);
            
            return true;
        }
    }
}
