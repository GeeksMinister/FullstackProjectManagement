﻿public class CurrencyData : ICurrencyData
{
    private readonly ISQLiteDataAccess _dbContext;

    public CurrencyData(ISQLiteDataAccess dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Currency>> GetAllCurrencies()
    {

        return await _dbContext.LoadData<Currency, object>("SELECT * FROM ExchangeRates", new { });
    }

    public async Task<Currency> UpdateCurrency(int id, Currency currency)
    {
        string query = "UPDATE ExchangeRates SET CurrencyName = @CurrencyName, USD = @USD, EUR = @EUR, GBP = @GBP," +
                                                "CAD = @CAD, CHF = @CHF, JPY = @JPY, UpdateDate = @UpdateDate";
        currency.Id = id;
        var result = await _dbContext.LoadData<Currency, object>(query, new { currency });
        return result.FirstOrDefault()!;
    }
}