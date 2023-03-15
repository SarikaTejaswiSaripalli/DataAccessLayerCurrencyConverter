namespace DataAccessLayerCurrencyConverter.Models
{
    public class CurrencyDetails
    {
        public decimal Amount { get; set; }
        public string? FromCurrencyCode { get; set; }
        public string? ToCurrencyCode { get; set; }
    }
}
