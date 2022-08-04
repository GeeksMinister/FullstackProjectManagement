public class EmployeeData : IEmployeeData
{
    private readonly ISQLiteDataAccess _dbContext;

    public EmployeeData(ISQLiteDataAccess dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        string query = "SELECT Id, FirstName, LastName, Birthdate, Phone, Email, City, Role FROM Employee";
        return await _dbContext.LoadData<Employee, object>(query, new { });
    }

    public async Task<Employee> GetEmployeeById(string Id)
    {
        string query = "SELECT Id, FirstName, LastName, Birthdate, Phone, Email, City, Role FROM Employee WHERE Id = @Id";
        var result = await _dbContext.LoadData<Employee, object>(query, new { Id });
        return result.FirstOrDefault()!;
    }

    public async Task DeleteEmployee(string Id)
    {
        string query = "DELETE FROM Employee WHERE Id = @Id";
        await _dbContext.SaveData(query, new { Id });
    }

    public async Task UpdateEmployee(Employee employee)
    {
        string query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Birthdate = @Birthdate," +
                       " Phone = @Phone, Email = @Email, City = @City, Role = @Role WHERE Id = @Id";
        await _dbContext.SaveData(query, employee);
    }

    public async Task<Employee>  InsertEmployee(Employee employee)
    {
        string query = "INSERT INTO Employee (Id, FirstName, LastName, Birthdate, Phone, Email, City, Role)" +
                       "VALUES(@Id, @FirstName, @LastName, @Birthdate, @Phone, @Email, @City, @Role)";
        var result = await _dbContext.LoadData<Employee, object>(query, employee);
        return result.FirstOrDefault()!;
    }
}
