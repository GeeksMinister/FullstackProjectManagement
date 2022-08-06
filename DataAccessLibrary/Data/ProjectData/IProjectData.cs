
public interface IProjectData
{
    Task DeleteProject(string Id);
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Project> GetProjectById(string Id);
    Task<Project> InsertProject(Project project);
    Task UpdateProject(Project project);
}