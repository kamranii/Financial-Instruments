using Microsoft.Extensions.Options;
using TestCase.Core.Configurations;
using TestCase.Core.Dtos;
using TestCase.Services.Interfaces;

namespace TestCase.Services
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly AlphaVantageApiConfiguration _config;

        public RequestBuilder(IOptions<AlphaVantageApiConfiguration> config)
        {
            _config = config.Value;
        }

        public string BuildRequestUrl(ConversionCurrencies currencies)
        {
            return $"{_config.BaseUrl}/query?function=CURRENCY_EXCHANGE_RATE&from_currency={currencies.FromCurrency}&to_currency={currencies.ToCurrency}&apikey={_config.ApiKey}";
        }
    }
}
