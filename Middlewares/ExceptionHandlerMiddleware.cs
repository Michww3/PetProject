using PetProject.Exceptions;

namespace PetProject.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (BaseException ex)
            {
                _logger.LogWarning(ex,
                    "Handled exception {ExceptionType} with status {StatusCode}. Path: {Path}",
                    ex.GetType().Name,
                    ex.StatusCode,
                    context.Request.Path);

                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message,
                    traceId = context.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception. Method: {Method}, Path: {Path}, TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal server error",
                    traceId = context.TraceIdentifier
                });
            }
        }
    }
}
