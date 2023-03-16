using BusinessLogicLayerCurrencyConverter.AuthenticationRepositories.IAuthenticationRepository;
using DataAccessLayerCurrencyConverter.AuthenticationModels;
using Microsoft.AspNetCore.Mvc;

namespace UserInterfaceCurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model) => Ok(await _authenticationService.Register(model));
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login model) => Ok(await _authenticationService.Login(model));
    }
}
