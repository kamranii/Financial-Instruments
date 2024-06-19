using TestCase.Core.Dtos;

namespace TestCase.Core.Interfaces
{
    public interface IPriceService
    {
        Task<PriceUpdate> GetPriceAsync(string instrument);
        Task<List<Instrument>> GetInstrumentsAsync();
    }
}
