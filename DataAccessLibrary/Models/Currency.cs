public sealed class Currency
{
    public int Id { get; set; }

    [JsonPropertyName("CurrencyName")]
    public string CurrencyName { get; set; } = string.Empty;
    
    [JsonPropertyName("updated")]
    public string UpdateDate { get; set; } = string.Empty;
    
    [DisplayName("USD")]
    [JsonPropertyName("USD_SEK")]
    public double USD { get; set; }
    
    [DisplayName("EUR")]
    [JsonPropertyName("EUR_SEK")]
    public double EUR { get; set; }
    
    [JsonPropertyName("GBP_SEK")]
    [DisplayName("GBP")]
    public double GBP { get; set; }
    
    [JsonPropertyName("CAD_SEK")]
    [DisplayName("CAD")]
    public double CAD { get; set; }

    [JsonPropertyName("CHF_SEK")]
    [DisplayName("CHF")]
    public double CHF { get; set; }

    [DisplayName("JPY")]
    [JsonPropertyName("JPY_SEK")]
    public double JPY { get; set; }

    [DisplayName("NOK")]
    [JsonPropertyName("NOK_SEK")]
    public double NOK { get; set; }

    [DisplayName("DKK")]
    [JsonPropertyName("DKK_SEK")]
    public double DKK { get; set; }

}


