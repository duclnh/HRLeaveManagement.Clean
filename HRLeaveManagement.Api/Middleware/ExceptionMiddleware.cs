﻿
using HRLeaveManagement.Api.Models;
using Newtonsoft.Json;
using System.Net;
using BadRequestException = HRLeaveManagement.Application.Exceptions.BadRequestException;
using NotFoundException = HRLeaveManagement.Application.Exceptions.NotFoundException;

namespace HRLeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Type = nameof(badRequestException),
                        Detail = badRequestException.InnerException?.Message,
                        Errors = badRequestException.ValidationErrors   
                    };  
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title =notFoundException.Message,
                        Status = (int)statusCode,
                        Type = nameof(notFoundException),
                        Detail = notFoundException.InnerException?.Message,
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(ex, logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
