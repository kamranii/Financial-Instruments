using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace TestCase.Logging
{
    public class HttpContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties.TryGetValue("HttpContext", out LogEventPropertyValue value) && value is ScalarValue scalarValue && scalarValue.Value is HttpContextInfo httpContext)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Protocol", httpContext.Protocol));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Scheme", httpContext.Scheme));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IpAddress", httpContext.IpAddress));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Host", httpContext.Host));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ApplicationName", httpContext.ApplicationName));
            }
        }

        public static class HttpContextEnricherHelper
        {
            public static HttpContextInfo GetHttpContextInfo(HttpContext httpContext)
            {
                var contextInfo = new HttpContextInfo
                {
                    Protocol = httpContext.Request.Protocol,
                    Scheme = httpContext.Request.Scheme,
                    IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                    Host = httpContext.Request.Host.ToString(),
                    ApplicationName = Assembly.GetCallingAssembly().GetName().Name
                };

                return contextInfo;
            }

            public static async void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
            {
                diagnosticContext.Set("HttpContext", GetHttpContextInfo(httpContext), true);
            }
        }
    }
}
