
public interface IEmployeeData
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetEmployeeById(string Id);
    Task DeleteEmployee(string Id);
    Task<Employee> InsertEmployee(Employee employeeDto);
    Task UpdateEmployee(Employee employee);
}