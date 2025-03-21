using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BreezyDrive.CommonService.Domain.Exceptions;
using MassTransit;

namespace BreezyDrive.CommonService.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (TryExtractFaultException(ex, out Exception customException))
                {
                    await HandleExceptionAsync(context, customException);
                    return;
                }
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            //var message = "An unexpected error occurred.";
            var message = exception.Message;
            var errorCode = "SERVER_ERROR";
            var details = exception.InnerException?.Message;

            switch (exception)
            {
                case CustomExceptions.InvalidDataException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = "INVALID_DATA";
                    break;

                case CustomExceptions.DataNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorCode = "DATA_NOT_FOUND";
                    break;

                case CustomExceptions.DataExistException:
                    statusCode = HttpStatusCode.Conflict;
                    errorCode = "DATA_EXISTS";
                    break;

                case CustomExceptions.UnAuthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = "UNAUTHORIZED";
                    break;

                case CustomExceptions.ForbbidenException:
                    statusCode = HttpStatusCode.Forbidden;
                    errorCode = "FORBIDDEN";
                    break;

                case CustomExceptions.InternalServerErrorException:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = "INTERNAL_SERVER_ERROR";
                    break;

                case AggregateException aggregateEx:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = "AGGREGATE_EXCEPTION";
                    message = string.Join("; ", aggregateEx.InnerExceptions.Select(e => e.Message));
                    break;

                default:
                    _logger.LogError(exception, "Unhandled exception occurred.");
                    break;
            }

            var response = new
            {
                statusCode = (int)statusCode,
                errorCode,
                message,
                details,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow
            };

            _logger.LogError("API Error: {StatusCode} - {ErrorCode} - {Message} - Path: {Path}",
                statusCode, errorCode, message, context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });

            await context.Response.WriteAsync(jsonResponse);
        }

        private bool TryExtractFaultException(Exception exception, out Exception customException)
        {
            customException = null;

            if (exception is RequestFaultException requestFaultException)
            {
                _logger.LogError("RequestFaultException detected: {Exception}", exception);

                var faultProperty = requestFaultException.GetType().GetProperty("Fault");
                if (faultProperty != null)
                {
                    var fault = faultProperty.GetValue(requestFaultException);
                    if (fault != null)
                    {
                        var exceptionsProperty = fault.GetType().GetProperty("Exceptions");
                        if (exceptionsProperty != null)
                        {
                            var faultExceptions = exceptionsProperty.GetValue(fault) as System.Collections.IEnumerable;
                            if (faultExceptions != null)
                            {
                                foreach (var faultEx in faultExceptions)
                                {
                                    string exceptionTypeName = faultEx.GetType().GetProperty("ExceptionType")
                                        ?.GetValue(faultEx)?.ToString();
                                    string message = faultEx.GetType().GetProperty("Message")?.GetValue(faultEx)
                                        ?.ToString();

                                    _logger.LogError("Extracted Exception: {ExceptionType} - {Message}",
                                        exceptionTypeName, message);

                                    // Kiểm tra nếu là exception của hệ thống
                                    if (!string.IsNullOrEmpty(exceptionTypeName) &&
                                        exceptionTypeName.StartsWith("BreezyDrive.CommonService.Domain.Exceptions"))
                                    {
                                        try
                                        {
                                            Type exceptionType = Type.GetType(exceptionTypeName);
                                            if (exceptionType != null &&
                                                typeof(Exception).IsAssignableFrom(exceptionType))
                                            {
                                                customException =
                                                    (Exception)Activator.CreateInstance(exceptionType, message);
                                                return true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.LogError("Error creating exception instance: {Error}", ex);
                                        }
                                    }

                                    // Nếu không tìm thấy exception, quăng lỗi chung
                                    customException = new Exception(message);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }


    }
}
