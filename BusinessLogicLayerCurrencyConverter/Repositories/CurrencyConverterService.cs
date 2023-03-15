using BusinessLogicLayerCurrencyConverter.Exceptions;
using BusinessLogicLayerCurrencyConverter.Repositories.IRepositories;

using DataAccessLayerCurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayerCurrencyConverter.Repositories
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly CurrencyConverterDbContext _currencyDbContext;
        private readonly ILogger<CurrencyConverterService> _logger;
        public CurrencyConverterService(CurrencyConverterDbContext currencyDbContext,ILogger<CurrencyConverterService> logger)
        {
            _currencyDbContext = currencyDbContext;
            _logger = logger;
        }
        public object GetConvertAmount(CurrencyDetails currencyDetails)
        {
            
            var transaction = _currencyDbContext.Database.BeginTransaction();
            
            try
            {
                _logger.LogInformation("logs are added for GetConvertAmount Method");
                CurrencyDetails currencyDetails1 = new CurrencyDetails
                {
                    Amount = currencyDetails.Amount,
                    FromCurrencyCode = currencyDetails.FromCurrencyCode,
                    ToCurrencyCode = currencyDetails.ToCurrencyCode
                };
                var rate = _currencyDbContext.ExchangeRates.FirstOrDefault(e=>e.FromCurrencyCode==currencyDetails.FromCurrencyCode && e.ToCurrencyCode==currencyDetails.ToCurrencyCode);
                if (rate == null)
                {
                    throw new InformationNotAvailableException("data is not avilable to convert " + currencyDetails.FromCurrencyCode + " to " + currencyDetails.ToCurrencyCode);
                }
                var convertedAmount = currencyDetails1.Amount * rate.Rate;
                return new { Amount = convertedAmount, Currency = currencyDetails.ToCurrencyCode };
            }
            catch (InformationNotAvailableException ex)
            {
                _logger.LogCritical(ex.Message);
                transaction.Commit();
                transaction.Rollback();

                throw new InformationNotAvailableException("currency code is not available");
            }
            finally
            {
                transaction.Dispose();
            }

        }
        public ExchangeRate PostInformation(ExchangeRate exchangeRate)
        {
            var transaction = _currencyDbContext.Database.BeginTransaction();
            try
            {
                _logger.LogInformation("logs are added for GetConvertAmount Method");
                var exchangeRates = new ExchangeRate
                {
                    FromCurrencyCode = exchangeRate.FromCurrencyCode,
                    ToCurrencyCode = exchangeRate.ToCurrencyCode,
                    Rate = exchangeRate.Rate
                };
                _currencyDbContext.ExchangeRates.Add(exchangeRates);
                _currencyDbContext.SaveChanges();
                return exchangeRate;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                transaction.Commit();
                transaction.Rollback();
                throw new InformationNotAvailableException(ex.Message);

            }
            finally { transaction.Dispose(); }

        }
        public List<ExchangeRate> GetAll()
        {
           return _currencyDbContext.ExchangeRates.ToList();
           
        }
        public void UpdateCurrencyRate(ExchangeRate exchangeRate)
        {
            //var transaction = _currencyDbContext.Database.BeginTransaction();
            try
            {
                _currencyDbContext.Entry(exchangeRate).State = EntityState.Modified;
                _currencyDbContext.SaveChanges();
            }
            catch (Exception)
            {
                //transaction.Rollback();
                //transaction.Commit();
                throw new InformationNotAvailableException("currency rate is not updated");
            }
            finally
            {
                //transaction.Dispose();
            }
        }
    }
}
