using System.Net.WebSockets;
using TestCase.Core.Interfaces;

namespace TestCase.Services
{
    public class WebSocketHandler
    {
        private readonly IPriceService _priceService;
        private readonly Dictionary<string, List<WebSocket>> _subscribers = new Dictionary<string, List<WebSocket>>();

        public WebSocketHandler(IPriceService priceService)
        {
            _priceService = priceService;
        }

        public async Task SubscribeAsync(string instrument, WebSocket webSocket)
        {
            if (string.IsNullOrWhiteSpace(instrument))
            {
                throw new ArgumentException("Instrument cannot be null or empty.");
            }

            if (webSocket == null)
            {
                throw new ArgumentNullException(nameof(webSocket));
            }

            if (!_subscribers.ContainsKey(instrument))
            {
                _subscribers[instrument] = new List<WebSocket>();
            }
            _subscribers[instrument].Add(webSocket);
        }

        public async Task BroadcastPriceUpdateAsync(string instrument)
        {
            var priceUpdate = await _priceService.GetPriceAsync(instrument);
            var message = System.Text.Json.JsonSerializer.Serialize(priceUpdate);
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);

            if (_subscribers.ContainsKey(instrument))
            {
                var subscribers = _subscribers[instrument];
                foreach (var subscriber in subscribers)
                {
                    if (subscriber.State == WebSocketState.Open)
                    {
                        await subscriber.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }

        public async Task StartBroadcastingAsync(string instrument)
        {
            while (true)
            {
                await BroadcastPriceUpdateAsync(instrument);
                await Task.Delay(5000);
            }
        }
    }
}
