using Serilog.Context;
using static TestCase.Logging.HttpContextEnricher;

namespace TestCase.Middlewares
{
    public class HttpContextLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var ctxInfo = HttpContextEnricherHelper.GetHttpContextInfo(httpContext);
            using (LogContext.PushProperty("HttpContext", ctxInfo, true))
            {
                await _next(httpContext);
            }
        }
    }
}
