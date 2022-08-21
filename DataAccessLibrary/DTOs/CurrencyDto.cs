namespace DataAccessLibrary.DTOs;

public sealed class CurrencyDto
{
    [JsonPropertyName("CurrencyName")]
    public string CurrencyName { get; set; } = string.Empty;

    [JsonPropertyName("updated")]
    public string UpdateDate { get; set; } = string.Empty;

    [DisplayName("USD")]
    [JsonPropertyName("USD_SEK")]
    public decimal USD { get; set; }

    [DisplayName("EUR")]
    [JsonPropertyName("EUR_SEK")]
    public decimal EUR { get; set; }

    [JsonPropertyName("GBP_SEK")]
    [DisplayName("GBP")]
    public decimal GBP { get; set; }

    [JsonPropertyName("CAD_SEK")]
    [DisplayName("CAD")]
    public decimal CAD { get; set; }

    [JsonPropertyName("CHF_SEK")]
    [DisplayName("CHF")]
    public decimal CHF { get; set; }

    [DisplayName("JPY")]
    [JsonPropertyName("JPY_SEK")]
    public decimal JPY { get; set; }

    [DisplayName("NOK")]
    [JsonPropertyName("NOK_SEK")]
    public decimal NOK { get; set; }

    [DisplayName("DKK")]
    [JsonPropertyName("DKK_SEK")]
    public decimal DKK { get; set; }


}


