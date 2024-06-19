using TestCase.Services;

namespace TestCase.Middlewares
{
    public class CustomWebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketHandler _webSocketHandler;

        public CustomWebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using (var webSocket = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        string instrument = context.Request.Query["instrument"];
                        await _webSocketHandler.SubscribeAsync(instrument, webSocket);
                        _ = _webSocketHandler.StartBroadcastingAsync(instrument);
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
