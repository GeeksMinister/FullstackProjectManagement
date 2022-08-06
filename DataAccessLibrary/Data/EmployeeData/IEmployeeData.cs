
public interface IEmployeeData
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetEmployeeById(string Id);
    Task DeleteEmployee(string Id);
    Task<Employee> InsertEmployee(Employee employeeDto);
    Task UpdateEmployee(Employee employee);
    //Task<byte[]> GetPasswordHash(string loginInfo);
    Task<string> GetPasswordAndSaltHex(string loginInfo);
    //Task<object> GetPasswordHash(string loginInfo);
    //Task<Employee> GetEmployeeByEmailOrId(string loginInfo);
    //Task<object> GetEmployeeByEmailOrId(string loginInfo);
    Task<Employee> GetEmployeeByEmailOrId(string loginInfo);
}