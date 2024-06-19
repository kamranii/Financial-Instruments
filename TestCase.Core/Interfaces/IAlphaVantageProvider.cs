using TestCase.Core.Dtos;

namespace TestCase.Core.Interfaces
{
    public interface IAlphaVantageProvider
    {
        Task<PriceUpdate> GetPriceAsync(string symbol);
        Task<List<Instrument>> GetAvailableInstrumentsAsync();
    }
}
