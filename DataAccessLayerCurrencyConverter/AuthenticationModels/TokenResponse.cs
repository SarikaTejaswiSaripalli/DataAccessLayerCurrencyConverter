using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerCurrencyConverter.AuthenticationModels
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
