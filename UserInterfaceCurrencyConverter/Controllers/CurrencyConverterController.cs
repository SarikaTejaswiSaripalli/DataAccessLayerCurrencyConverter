
using BusinessLogicLayerCurrencyConverter.Repositories.IRepositories;

using DataAccessLayerCurrencyConverter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace UserInterfaceCurrencyConverter.Controllers
{
    //[Authorize]   
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ICurrencyConverterService _currencyConverterService;
        public CurrencyConverterController(ICurrencyConverterService currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }
        [HttpPost("convert")]
        public IActionResult Convert([FromBody]CurrencyDetails currencyDetails) => Ok(_currencyConverterService.GetConvertAmount(currencyDetails));
        [HttpPost("InsertInfo")]
        public async Task<IActionResult> PostData(ExchangeRate exchangeRate) => Ok( await _currencyConverterService.PostInformation(exchangeRate));
        [HttpGet("ListOfCountries")]
        public IActionResult GetAllDetails() => Ok(_currencyConverterService.GetAll());
        [HttpPut("UpdateCurrencyRate")]
        public  IActionResult UpdateCurrency(ExchangeRate exchangeRate) => Ok(_currencyConverterService.UpdateCurrencyRate(exchangeRate));
        
    }
}
