namespace DataAccessLibrary.Data.EmployeeData;

public class EmployeeData : IEmployeeData
{
    private readonly ISQLiteDataAccess _dbContext;
    private readonly IConfiguration _config;

    public EmployeeData(ISQLiteDataAccess dbContext, IConfiguration config )
    {
        _dbContext = dbContext;
        _config = config;
    }

    public async Task<Employee> GetAllInfo(string id)
    {
        string query = "SELECT Employee.Id, Employee.FirstName, Employee.LastName, Employee.Birthdate," +
" Employee.Phone, Employee.Email, Employee.City, Employee.Role, Todo.Id, Todo.TaskName, Todo.Description," +
" Todo.Priority, Todo.Status, Todo.EmployeeId, Todo.ProjectId, Project.Id, Project.ProjectName," +
" Project.Description, Project.Customer, Project.Status" +
" FROM Employee JOIN Todo ON Employee.Id = Todo.EmployeeId" +
" JOIN Project ON Todo.ProjectId = Project.Id" +
" WHERE Employee.Id = @Id";

        using IDbConnection connection = new SqliteConnection(_config.GetConnectionString("Default"));
        List<Todo> temp_todos = new List<Todo>();
        var result = await connection.QueryAsync<Employee, Todo, Project, Employee >(query, (emp, todo, proj) =>
        {
            todo.Employee ??= emp;
            todo.Project ??= proj;

            temp_todos.Add(todo);
            return emp;
        }, new { Id = id });
        
        var employe = result?.FirstOrDefault();
        if (employe is null) return null!;
        employe!.Todos = temp_todos;
        return employe;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        string query = "SELECT Id, FirstName, LastName, Birthdate, Phone, Email, City, Role FROM Employee";
        return await _dbContext.LoadData<Employee, object>(query, new { });
    }

    public async Task<Employee> GetEmployeeById(string id)
    {
        string query = "SELECT Id, FirstName, LastName, Birthdate, Phone, Email, City, Role " +
                       "FROM Employee WHERE Id = @Id";
        var result = await _dbContext.LoadData<Employee, object>(query, new { Id = id });
        return result.FirstOrDefault()!;
    }

    public async Task<bool> CheckEmployee(string id)
    {
        string query = "SELECT TRUE FROM Employee WHERE Id = @Id";
        var result = await _dbContext.LoadData<bool, object>(query, new {Id = id});
        return result.FirstOrDefault();
    }

    public async Task<Employee> GetEmployeeByEmailOrId(string loginInfo)
    {
        string query = "SELECT Id, FirstName, LastName, Birthdate, Phone, Email, City, " +
                       "Role FROM Employee WHERE Email = @LoginInfo OR Id = @LoginInfo";
        var result = await _dbContext.LoadData<Employee, object>(query, new { LoginInfo = loginInfo });
        return result.FirstOrDefault()!;
    }

    public async Task<string> GetPasswordAndSaltHex(string loginInfo)
    {
        string query = "SELECT HEX(PasswordSalt) || '_' || HEX(PasswordHash) " +
                       "FROM Employee WHERE Id = @LoginInfo Or Email = @LoginInfo";
        var result = await _dbContext.LoadData<string, object>(query, new { LoginInfo = loginInfo });
        return result.FirstOrDefault()!;
    }

    public async Task DeleteEmployee(string id)
    {
        string query = "DELETE FROM Employee WHERE Id = @Id";
        await _dbContext.SaveData(query, new { Id = id });
    }

    public async Task UpdateEmployee(Employee employee)
    {
        string query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, " +
"Birthdate = Substring(@Birthdate, 0, 11), Phone = @Phone, Email = @Email, City = @City, Role = @Role," +
" PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt WHERE Id = @Id";
        await _dbContext.SaveData(query, employee);
    }

    public async Task<Employee> InsertEmployee(Employee employee)
    {
        string query = "INSERT INTO Employee (Id, FirstName, LastName, Birthdate, Phone, Email, " +
"City, Role, PasswordHash, PasswordSalt) VALUES(@Id, @FirstName, @LastName, Substring(@Birthdate, 0, 11), @Phone," +
" @Email, @City, @Role, @PasswordHash, @PasswordSalt)";
        var result = await _dbContext.LoadData<Employee, object>(query, employee);
        return result.FirstOrDefault()!;
    }
}
