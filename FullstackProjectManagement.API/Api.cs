namespace FullstackProjectManagement.API;

public static class Api
{

    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Employee", GetAllEmployee);
    }

    private static async Task<IResult> GetAllEmployee(IEmployeeRepository data)
    {
        try
        {
            return Results.Ok(await data.GetAll());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
