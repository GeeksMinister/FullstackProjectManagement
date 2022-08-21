namespace DataAccessLibrary.DbAccess;

public interface ISQLiteDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string query, U parameters, string connectionId = "Default");
    Task SaveData<T>(string query, T parameters, string connectionId = "Default");
}