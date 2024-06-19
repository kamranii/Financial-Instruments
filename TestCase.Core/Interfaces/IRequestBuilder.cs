
using TestCase.Core.Dtos;

namespace TestCase.Services.Interfaces
{
    public interface IRequestBuilder
    {
        string BuildRequestUrl(ConversionCurrencies currencies);
    }
}
