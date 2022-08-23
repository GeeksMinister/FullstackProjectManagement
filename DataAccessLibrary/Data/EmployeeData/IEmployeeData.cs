namespace DataAccessLibrary.Data.EmployeeData;

public interface IEmployeeData
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetEmployeeById(string Id);
    Task DeleteEmployee(string Id);
    Task<Employee> InsertEmployee(Employee employeeDto);
    Task UpdateEmployee(Employee employee);
    Task<string> GetPasswordAndSaltHex(string loginInfo);
    Task<Employee> GetEmployeeByEmailOrId(string loginInfo);
    Task<Employee> GetAllInfo(string id);
    Task<bool> CheckEmployee(string id);
}