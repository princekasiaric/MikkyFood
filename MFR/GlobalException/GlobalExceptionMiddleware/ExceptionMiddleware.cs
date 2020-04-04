using MFR.Core.DTO.Response;
using MFR.Core.Utils;
using MFR.Persistence.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MFR.GlobalException.GlobalExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate request, LoggerFactory logger)
        {
            _request = request;
            _logger = logger.CreateLogger(typeof(Type));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HabdleExceptionAsync(context, ex);
            }
            catch (EmptyShoppingBasketException ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HabdleExceptionAsync(context, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HabdleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HabdleExceptionAsync(context, ex);
            }
        }

        private Task HabdleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(new ApiResponse
            {
                Status = false,
                Message = exception.Message
            }.ToString());
        }
    }
}
