
using BusinessLogicLayerCurrencyConverter.Repositories.IRepositories;
using DataAccessLayerCurrencyConverter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace UserInterfaceCurrencyConverter.Controllers
{
    [Authorize]
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
        public IActionResult PostData(ExchangeRate exchangeRate) => Ok(_currencyConverterService.PostInformation(exchangeRate));
        [HttpGet("ListOfCountries")]
        public IActionResult GetAllDetails() => Ok(_currencyConverterService.GetAll());
        [HttpPut("UpdateCurrencyRate")]
        public async Task<ActionResult<ExchangeRate>> UpdateCollege(string fromCurrencyCode,string toCurrencyCode, ExchangeRate exchangeRate)
        {
            _currencyConverterService.UpdateCurrencyRate(exchangeRate);
            return await Task.FromResult(exchangeRate);
        }

    }
}
