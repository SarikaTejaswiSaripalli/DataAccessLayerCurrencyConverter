

using DataAccessLayerCurrencyConverter.Models;

namespace BusinessLogicLayerCurrencyConverter.Repositories.IRepositories
{
    public interface ICurrencyConverterService
    {
        object GetConvertAmount(CurrencyDetails currencyetails);
        ExchangeRate PostInformation(ExchangeRate exchangeRate);
        List<ExchangeRate> GetAll();
        void UpdateCurrencyRate(ExchangeRate exchangeRate);
    }
}
