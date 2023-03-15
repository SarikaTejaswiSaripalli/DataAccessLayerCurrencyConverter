using System;
using System.Collections.Generic;

namespace DataAccessLayerCurrencyConverter.Models;

public partial class ExchangeRate
{

    public int Id { get; set; }

    public string FromCurrencyCode { get; set; } = null!;

    public string ToCurrencyCode { get; set; } = null!;

    public decimal Rate { get; set; }
}
