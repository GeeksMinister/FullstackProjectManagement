namespace DataAccessLibrary.Data.ProjectData;

public class ProjectData : IProjectData
{
    private readonly ISQLiteDataAccess _dbContext;

    public ProjectData(ISQLiteDataAccess dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        string query = "SELECT Id, ProjectName, Description, Customer, Status FROM Project";
        return await _dbContext.LoadData<Project, object>(query, new { });
    }

    public async Task<IEnumerable<Project>> GetProjectNames()
    {
        string query = "SELECT Id, ProjectName FROM Project";
        return await _dbContext.LoadData<Project, object>(query, new { });
    }

    public async Task<Project> GetProjectById(string Id)
    {
        string query = "SELECT Id, ProjectName, Description, Customer, Status FROM Project WHERE Id = @Id";
        var result = await _dbContext.LoadData<Project, object>(query, new { Id });
        return result.FirstOrDefault()!;
    }


    public async Task DeleteProject(string Id)
    {
        string query = "DELETE FROM Project WHERE Id = @Id";
        await _dbContext.SaveData(query, new { Id });
    }

    public async Task UpdateProject(Project project)
    {
        string query = "UPDATE Project SET ProjectName = @ProjectName, Description = @Description, Customer = @Customer," +
                       " Status = @Status WHERE Id = @Id";
        await _dbContext.SaveData(query, project);
    }

    public async Task<Project> InsertProject(Project project)
    {
        string query = "INSERT INTO Project (Id, ProjectName, Description, Customer, Status)" +
                       "VALUES(@Id, @ProjectName, @Description, @Customer, @Status)";
        var result = await _dbContext.LoadData<Project, object>(query, project);
        return result.FirstOrDefault()!;
    }
}
