namespace CatalogService.Middleware
{
    using Serilog.Context;

    public class LoggerEnricherMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            var userId = context.User?.FindFirst("sub")?.Value ?? "anonymous";
        
            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("UserId", userId))
            {
                await next(context);
            }
        }
    }
}