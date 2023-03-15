using DataAccessLayerCurrencyConverter.AuthenticationModels;

namespace BusinessLogicLayerCurrencyConverter.AuthenticationRepositories.IAuthenticationRepository
{
    public interface IAuthenticationService
    {
        Task<string> Register(Register model);
        Task<TokenResponse> Login(Login model);
    }
}
