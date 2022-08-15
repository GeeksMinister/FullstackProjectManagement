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

    [Get("/Employee/Tasks")]
    Task<List<Todo>> GetAllTasks();

    [Get("/Employee/{emp_Id}/Tasks")]
    Task<List<Todo>> GetEmployeeTasks(string emp_Id);

    [Get("/Employee/{emp_Id}/Tasks/Count")]
    Task<int> GetEmployeeTasksCount(string emp_Id);

}
