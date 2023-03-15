using BusinessLogicLayerCurrencyConverter.AuthenticationRepositories.IAuthenticationRepository;
using BusinessLogicLayerCurrencyConverter.Exceptions;
using DataAccessLayerCurrencyConverter.AuthenticationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayerCurrencyConverter.AuthenticationRepositories
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
       
        public AuthenticationService(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            
        }
        public async Task<string> Register(Register model)
        {
            try
            {
               
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    return "User Already Exists";
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return "User creation failed! Please check user details and try again.";
                }
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
               
                switch (model.Role)
                {
                    case "Admin":
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                        break;
                    case "User":
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                        break;
                    
                    default:
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                        break;
                }
                return "User Created Sucessfully";
            }
            catch (InformationNotAvailableException)
            {
               
                throw new InformationNotAvailableException("Something Went Wrong!!!!");
            }
        }
        public async Task<TokenResponse> Login(Login model)
        {
            try
            {
               
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var token = GetToken(authClaims);
                    TokenResponse tokenResponse = new TokenResponse();
                    tokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    tokenResponse.Expire = DateTime.UtcNow.AddMinutes(30);
                    return tokenResponse;
                }
                return null;
            }
            catch (BadRequestException)
            {
                throw new BadRequestException("Something Went Wrong!!!");
               
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            try
            {
                
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken
                    (
                      issuer: _configuration["JWT:ValidIssuer"],
                      audience: _configuration["JWT:ValidAudience"],
                      expires: DateTime.Now.AddHours(3),
                      claims: authClaims,
                      signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            catch (BadRequestException)
            {
                throw new BadRequestException("Something Went Wrong!!!");
               
            }
        }
    }
}
