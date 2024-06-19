using Serilog;
using System.Text.Json;
using TestCase.Core.Dtos;
using TestCase.Core.Interfaces;
using TestCase.Services.Interfaces;

namespace TestCase.Infra.DataProviders
{
    public class AlphaVantageProvider : IAlphaVantageProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestBuilder _requestBuilder;

        public AlphaVantageProvider(HttpClient httpClient,
                                    IRequestBuilder requestBuilder)
        {
            _httpClient = httpClient;
            _requestBuilder = requestBuilder;
        }

        public async Task<List<Instrument>> GetAvailableInstrumentsAsync()
        {

            return InstrumentSeedData.Instruments;
        }

        public async Task<PriceUpdate> GetPriceAsync(string symbol)
        {
            var currencies = GetCurrencies(symbol);
            var url = _requestBuilder.BuildRequestUrl(currencies);
            //var response = await _httpClient.GetStringAsync(url);
            var response = "{\r\n    \"Realtime Currency Exchange Rate\": {\r\n        \"1. From_Currency Code\": \"BTC\",\r\n        \"2. From_Currency Name\": \"Bitcoin\",\r\n        \"3. To_Currency Code\": \"EUR\",\r\n        \"4. To_Currency Name\": \"Euro\",\r\n        \"5. Exchange Rate\": \"60807.57000000\",\r\n        \"6. Last Refreshed\": \"2024-06-18 14:42:19\",\r\n        \"7. Time Zone\": \"UTC\",\r\n        \"8. Bid Price\": \"60807.09200000\",\r\n        \"9. Ask Price\": \"60809.85300000\"\r\n    }\r\n}";
            if (string.IsNullOrWhiteSpace(response))
            {
                throw new HttpRequestException("API response was empty.");
            }

            var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response);
            var exchangeRateData = jsonData["Realtime Currency Exchange Rate"];

            var rate = exchangeRateData.GetProperty("5. Exchange Rate").ToString();
            if (!decimal.TryParse(rate, out var price))
            {
                throw new FormatException("Invalid exchange rate format.");
            }

            var priceUpdate = new PriceUpdate
            {
                Symbol = symbol,
                Price = price,
                TimeStamp = DateTime.UtcNow
            };

            return priceUpdate;
        }

        private ConversionCurrencies GetCurrencies(string symbol)
        {
            return symbol.ToUpper() switch
            {
                "EURUSD" => new ConversionCurrencies("EUR", "USD"),
                "USDJPY" => new ConversionCurrencies("USD", "JPY"),
                "BTCUSD" => new ConversionCurrencies("BTC", "USD"),
                _ => throw new ArgumentException("Invalid symbol")
            };
        }
    }
}
