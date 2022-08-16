public interface IEmployeeClientData
{
    [Get("/Employee")]
    Task<IEnumerable<Employee>> GetAllEmployees();

    [Get("/Employee/{id}")]
    Task<Employee> GetEmployeeById(string id);

    [Post("/Employee")]
    Task<Employee> InsertEmployee(EmployeeDto employeeDto);

    [Put("/Employee/{id}")]
    Task UpdateEmployee(string id, EmployeeDto employeeDto);

    [Delete ("/Employee")]
    Task DeleteEmployee(string id);


}
