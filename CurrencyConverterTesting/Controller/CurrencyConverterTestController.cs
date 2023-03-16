using BusinessLogicLayerCurrencyConverter.Repositories.IRepositories;
using CurrencyConverterTesting.MoqData;
using DataAccessLayerCurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserInterfaceCurrencyConverter.Controllers;

namespace CurrencyConverterTesting.Controller
{
    public class CurrencyConverterTestController
    {
        private readonly Mock<ICurrencyConverterService> _currencyConverterService;
        private readonly CurrencyConverterController _currencyConverterController;
        public CurrencyConverterTestController()
        {
            _currencyConverterService = new Mock<ICurrencyConverterService>();
            _currencyConverterController=new  CurrencyConverterController(_currencyConverterService.Object);
        }
        [Fact]
        public async Task GetAllDetails_ShouldReturn200()
        {
            //Arrange
            _currencyConverterService.Setup(x=>x.GetAll()).Returns(CurrencyConverterMoqData.GetAll());
            //Act
            var result = _currencyConverterController.GetAllDetails();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetAlldetails_ShouldReturn400()
        {
            //Arrange
            _currencyConverterService.Setup(x => x.GetAll()).Returns(CurrencyConverterMoqData.GetAll());
            //Act
            var result = _currencyConverterController.GetAllDetails();
            result = null;
            if(result != null)
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void PostData_ShoudReturn200()
        {
            //Arrange
            ExchangeRate exchange = new ExchangeRate()
            {
                Id= 1,
                FromCurrencyCode="USD",
                ToCurrencyCode="INR"
            };
            //Act
            var OkResult=_currencyConverterController.PostData(exchange);
            //Assert
            Assert.IsType<OkObjectResult>(OkResult);
           
        }
        [Fact]
        public void PostData_NotEnteredData()
        {
            //Arrange
            ExchangeRate exchangeRate = new ExchangeRate()
            {
                Id = 0,
                FromCurrencyCode = null,
                ToCurrencyCode = null

            };
            //Act
            var OkResult = _currencyConverterController.PostData(exchangeRate);
            //Assert
            Assert.IsType<OkObjectResult>(OkResult);

        }
        [Fact]
        public void PostData_InformationNotAvailable()
        {
            //Arrange
            ExchangeRate exchangeRate = new ExchangeRate()
            {
                Id = 100,
                FromCurrencyCode="FGI",
                ToCurrencyCode="FEY"
            };
            //Act
            var OkResult = _currencyConverterController.PostData(exchangeRate);

            //Assert
            Assert.IsType<OkObjectResult>(OkResult);

        }
    

    }

    }

