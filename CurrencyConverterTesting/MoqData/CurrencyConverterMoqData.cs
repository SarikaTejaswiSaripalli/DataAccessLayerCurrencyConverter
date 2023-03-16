using DataAccessLayerCurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterTesting.MoqData
{
    public class CurrencyConverterMoqData
    {
        public static List<ExchangeRate> GetAll()
        {
            return new List<ExchangeRate>
            {
                new ExchangeRate
                {
                    Id = 1,
                    FromCurrencyCode="USD",
                    ToCurrencyCode="INR",
                    Rate=82,
                }
                       
            };
        }
        public static ExchangeRate PostData(ExchangeRate exchangeRate)
        {
            return new ExchangeRate
            {
                Id = 2,
                FromCurrencyCode = "INR",
                ToCurrencyCode = "USD"
            };
        }
        

    }
    
    }

