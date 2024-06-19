using TestCase.Core.Dtos;

namespace TestCase.Infra
{
    public class InstrumentSeedData
    {
        public static List<Instrument> Instruments { get; } = new List<Instrument>
        {
            new Instrument { Symbol = "EURUSD", Name = "Euro to US Dollar" },
            new Instrument { Symbol = "USDJPY", Name = "US Dollar to Japanese Yen" },
            new Instrument { Symbol = "BTCUSD", Name = "Bitcoin to US Dollar" }
        };
    }
}
