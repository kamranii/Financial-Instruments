namespace TestCase.Core.Dtos
{
    public class ConversionCurrencies
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }

        public ConversionCurrencies(string from, string to) { 
            FromCurrency = from;
            ToCurrency = to;
        }
    }
}
