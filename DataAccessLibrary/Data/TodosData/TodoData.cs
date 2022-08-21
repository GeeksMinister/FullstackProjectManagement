namespace DataAccessLibrary.Data.TodosData;

public class TodoData : ITodoData
{
    private readonly ISQLiteDataAccess _dbContext;

    public TodoData(ISQLiteDataAccess dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Todo>> GetAllTasks()
    {
        string query = "SELECT Id, TaskName, Description, Priority, Status, EmployeeId, ProjectId FROM Todo";
        return await _dbContext.LoadData<Todo, object>(query, new { });
    }

    public async Task<IEnumerable<Todo>> GetEmployeeTasks(string emp_Id)
    {
        string query = "SELECT Id, TaskName, Description, Priority, Status, " +
"(SELECT Project.ProjectName FROM Project WHERE Project.Id = Todo.ProjectId) as 'ProjectId' "+
"FROM Todo WHERE EmployeeId = @Emp_Id";
        return await _dbContext.LoadData<Todo, object>(query, new { Emp_Id = emp_Id });
    }

    public async Task<Todo> GetTaskById(string id)
    {
        string query = "SELECT Id, TaskName, Description, Priority, Status, " +
"(SELECT Project.ProjectName FROM Project WHERE Project.Id = Todo.ProjectId) as 'ProjectId' " +
"FROM Todo WHERE Id = @Id";
        var result = await _dbContext.LoadData<Todo, object>(query, new { Id = id });
        return result.FirstOrDefault()!;
    }

    public async Task<int> GetEmployeeTasksCount(string em_Id)
    {
        string query = "SELECT COUNT(TaskName) FROM Todo WHERE EmployeeId = @Emp_Id";
        var result = await _dbContext.LoadData<int, object>(query, new {Emp_Id = em_Id});
        return result.FirstOrDefault();
    }

    public async Task<Todo> InsertTask(Todo todo)
    {
        string query = "INSERT INTO Todo (Id, TaskName, Description, Priority, Status, EmployeeId, ProjectId)" +
                       "VALUES(@Id, @TaskName, @Description, @Priority, @Status, @EmployeeId, @ProjectId)";
        var result = await _dbContext.LoadData<Todo, object>(query, todo);
        return result.FirstOrDefault()!;
    }
    public async Task UpdateTask(Todo todo)
    {
        string query = "UPDATE Todo SET TaskName = @TaskName, Description = @Description, Priority = @Priority," +
                       " Status = @Status, EmployeeId = @EmployeeId, ProjectId = @ProjectId WHERE Id = @Id";
        await _dbContext.SaveData(query, todo);
    }

    public async Task DeleteTask(string Id)
    {
        string query = "DELETE FROM Todo WHERE Id = @Id";
        await _dbContext.SaveData(query, new { Id });
    }

    //Insert ignore





    //Insert Force

}
