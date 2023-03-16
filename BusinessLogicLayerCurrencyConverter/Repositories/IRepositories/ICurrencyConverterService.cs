

using DataAccessLayerCurrencyConverter.Models;

namespace BusinessLogicLayerCurrencyConverter.Repositories.IRepositories
{
    public interface ICurrencyConverterService
    {
        object GetConvertAmount(CurrencyDetails currencyetails);
       Task< ExchangeRate> PostInformation(ExchangeRate exchangeRate);
        List<ExchangeRate> GetAll();
        ExchangeRate UpdateCurrencyRate(ExchangeRate exchangeRate);


    }
}
