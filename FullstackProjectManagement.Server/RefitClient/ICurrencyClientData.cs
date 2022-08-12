public interface ICurrencyClientData
{
    [Get("/ExchangeRates")]
    Task<IEnumerable<Currency>> GetAllCurrencies();

    [Put("/ExchangeRates/{id}")]
    Task<Currency> UpdateCurrency([Header("Key")] string Key, Currency currency, int id = 1);

    
}

