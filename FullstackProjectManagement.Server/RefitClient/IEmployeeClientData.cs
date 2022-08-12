﻿public interface IEmployeeClientData
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

//app.MapPost("/Login", Login);
//app.MapGet("/Employee", GetAllEmployees);
//app.MapGet("/Employee/{id}", GetEmployeeById);
//app.MapPost("/Employee", InsertEmployee);
//app.MapPut("/Employee", UpdateEmployee);
//app.MapDelete("/Employee", DeleteEmployee);
//app.MapGet("/ExchangeRates", GetAllCurrencies);
//app.MapPut("/ExchangeRates", UpdateCurrency);

//public interface IEmployeeData
//{
//    Task<IEnumerable<Employee>> GetAllEmployees();
//    Task<Employee> GetEmployeeById(string Id);
//    Task DeleteEmployee(string Id);
//    Task<Employee> InsertEmployee(Employee employeeDto);
//    Task<string> GetPasswordAndSaltHex(string loginInfo);
//    Task<Employee> GetEmployeeByEmailOrId(string loginInfo);
//}