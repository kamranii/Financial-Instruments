using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using TestCase.Core.Dtos;

namespace TestCase.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new ErrorResponseDto();
            errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ArgumentException argEx)
            {
                errorResponse.Message = argEx.Message;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is WebSocketException wsEx)
            {
                errorResponse.Message = "A WebSocket error occurred: " + wsEx.Message;
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                errorResponse.Message = "An unexpected error occurred. Please try again later.";
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;

            _logger.LogError(exception, errorResponse.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
