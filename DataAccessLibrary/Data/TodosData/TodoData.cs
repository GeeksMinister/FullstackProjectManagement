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
        string query = "SELECT Id, TaskName, Description, Priority, Status, EmployeeId, " +
"(SELECT Project.ProjectName FROM Project WHERE Project.Id = Todo.ProjectId) as 'ProjectId' "+
"FROM Todo WHERE EmployeeId = @Emp_Id";
        return await _dbContext.LoadData<Todo, object>(query, new { Emp_Id = emp_Id });
    }

    public async Task<int> GetEmployeeTasksCount(string em_Id)
    {
        string query = "SELECT COUNT(TaskName) FROM Todo WHERE EmployeeId = @Emp_Id";
        var result = await _dbContext.LoadData<int, object>(query, new {Emp_Id = em_Id});
        return result.FirstOrDefault();
    }




    //Insert ignore





    //Insert Force

}
