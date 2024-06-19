namespace TestCase.Core.Configurations
{
    public record AlphaVantageApiConfiguration
    {
        public string BaseUrl { get; init; }
        public string ApiKey { get; init; }
    }
}
