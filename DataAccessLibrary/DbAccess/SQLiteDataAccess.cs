﻿namespace DataAccessLibrary.DbAccess;

public class SQLiteDataAccess : ISQLiteDataAccess
{
    private readonly IConfiguration _config;

    public SQLiteDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task SaveData<T>(string query, T parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqliteConnection(_config.GetConnectionString(connectionId));
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string query, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqliteConnection(_config.GetConnectionString(connectionId));
        return await connection.QueryAsync<T>(query, parameters);
    }
}
