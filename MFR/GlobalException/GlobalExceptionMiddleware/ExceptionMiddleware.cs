﻿using MFR.Core.DTO.Response;
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
                await HandleExceptionAsync(context, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
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
