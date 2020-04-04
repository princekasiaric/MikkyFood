using MFR.GlobalException.GlobalExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace MFR.GlobalException
{
    public static class ExceptionExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionMiddleware>(loggerFactory);
        }
    }
}
