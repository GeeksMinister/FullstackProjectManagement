
public interface ICurrencyData
{
    Task<IEnumerable<Currency>> GetAllCurrencies();
    Task<Currency> GetCurrencyById(int id);
    Task<Currency> UpdateCurrency(int id, Currency currency);
}