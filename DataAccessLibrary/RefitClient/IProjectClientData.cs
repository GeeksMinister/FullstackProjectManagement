namespace DataAccessLibrary.RefitClient;

public interface IProjectClientData
{
    [Get("/Project/Names")]
    Task<IEnumerable<Project>> GetProjectNames();
}

