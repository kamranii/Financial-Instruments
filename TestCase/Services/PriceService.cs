using TestCase.Core.Dtos;
using TestCase.Core.Interfaces;

namespace TestCase.Services
{
    public class PriceService : IPriceService
    {
        private readonly IAlphaVantageProvider _alphaVantageProvider;

        public PriceService(IAlphaVantageProvider alphaVantageProvider)
        {
            _alphaVantageProvider = alphaVantageProvider;
        }

        public async Task<PriceUpdate> GetPriceAsync(string instrument)
        {
            return await _alphaVantageProvider.GetPriceAsync(instrument);
        }

        public async Task<List<Instrument>> GetInstrumentsAsync()
        {
            return await _alphaVantageProvider.GetAvailableInstrumentsAsync();
        }
    }
}
