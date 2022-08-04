namespace FullstackProjectManagement.API;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Employee", GetAllEmployees);
        app.MapGet("/Employee/{id}", GetEmployeeById);
        app.MapPost("/Employee", InsertEmployee);
        app.MapDelete("/Employee", DeleteEmployee);
    }

    private static async Task<IResult> GetAllEmployees(IEmployeeData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllEmployees();
            if (result?.Any() == false) return Results.NotFound();
            var totalEmployees = result?.ToList().Count.ToString();
            context.Response.Headers.Add("Total_Employee", totalEmployees);
            return Results.Ok(await data.GetAllEmployees());
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
            return Results.Ok($"Employee with Id: {id} was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


}
