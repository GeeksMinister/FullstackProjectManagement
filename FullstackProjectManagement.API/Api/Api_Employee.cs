using System.Collections.Generic;
using System.Security.Cryptography;

public static class Api_Employee
{
    public static void ConfigureApiEmployee(this WebApplication app)
    {
        app.MapGet("/Employee", GetAllEmployees);
        app.MapPost("/Login", Login);
        app.MapGet("/Employee/{id}", GetEmployeeById);
        app.MapPost("/Employee", InsertEmployee);
        app.MapPut("Employee", UpdateEmployee);
        app.MapDelete("/Employee", DeleteEmployee);
    }

    private static async Task<IResult> Login(string loginInfo, string password, IEmployeeData data)
    {
        var employee = await data.GetEmployeeByEmailOrId(loginInfo);
        if (employee == null) return Results.NotFound();

        (var passwordHash, var passwordSalt) = await GetPasswordAndSaltHash(loginInfo, data);
        if (VerifyPassword(password, passwordSalt, passwordHash) == false)
        {
            return Results.BadRequest("Wrong Password");
        }
        return Results.Ok($"Successful Login!  Welcome {employee.FirstName} {employee.LastName}");
    }

    //[Authorize]
    private static async Task<IResult> GetAllEmployees(IEmployeeData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllEmployees();
            if (result?.Any() == false) return Results.NotFound();
            var totalEmployees = result?.ToList().Count.ToString();
            context.Response.Headers.Add("Total_Employees", totalEmployees);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetEmployeeById(string id, IEmployeeData data)
    {
        try
        {
            var result = await data.GetEmployeeById(id);
            if(result is null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertEmployee(EmployeeDto employeeDto, IEmployeeData data)
    {
        try
        {
            if(employeeDto == null) return Results.BadRequest();
            var employee = new Employee();
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Birthdate = employeeDto.Birthdate;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.City = employeeDto.City;
            employee.Role = employeeDto.Role;
            await data.InsertEmployee(employee);
            return Results.Ok(employee);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteEmployee(string id, IEmployeeData data)
    {
        try
        {
            var employee = await data.GetEmployeeById(id);
            if(employee == null) return Results.NotFound();
            await data.DeleteEmployee(id);
            return Results.Ok($"Employee with Id: {id} Was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateEmployee(string id, EmployeeDto employeeDto, IEmployeeData data)
    {
        try
        {
            var employee = await data.GetEmployeeById(id);
            if (employee == null) return Results.NotFound();
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Birthdate = employeeDto.Birthdate;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.City = employeeDto.City;
            employee.Role = employeeDto.Role;
            await data.UpdateEmployee(employee);
            return Results.Ok(employee);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static (byte[] passwordSalt, byte[] passwordHash) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        return (passwordSalt: hmac.Key, passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }


    private static async Task<(byte[] passwordSalt, byte[] passwordHash)> GetPasswordAndSaltHash(string loginInfo, IEmployeeData data)
    {
        var hexValues = await data.GetPasswordAndSaltHex(loginInfo);
        string passwordHex = hexValues.Split("_")[0];
        string saltHex = hexValues.Split("_")[1];
        return (Convert.FromHexString(passwordHex), Convert.FromHexString(saltHex));
    }


}
