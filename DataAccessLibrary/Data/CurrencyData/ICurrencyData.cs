
public interface ICurrencyData
{
    Task<IEnumerable<Currency>> GetAllCurrencies();
    Task<Currency> UpdateCurrency(int id, Currency currency);
}