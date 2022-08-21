namespace DataAccessLibrary.Models;

public class Price
{
    public string FromCurrency { get; set; } = "USD";
    public string ToCurrency { get; set; } = "SEK";

    [Range(1, double.MaxValue, ErrorMessage = "Please type in the amount")]
    public decimal Amount { get; set; } = 1;
    public string Result { get; set; } = string.Empty;
}