public static class Api_Todo
{
    public static void ConfigureApiEmployeeTasks(this WebApplication app)
    {
        app.MapGet("/Employees/Tasks", GetAllTasks);
        app.MapGet("/Employee/{emp_Id}/Tasks", GetEmployeeTasks);
        app.MapGet("/Employee/{emp_Id}/Tasks/Count", GetEmployeeTasksCount);
    }

    private static async Task<IResult> GetAllTasks(ITodoData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllTasks();
            if (result?.Any() == false) return Results.NotFound();
            var totalTasks = result?.ToList().Count;
            context.Response.Headers.Add("Total_Tasks", totalTasks.ToString());
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetEmployeeTasks(string emp_Id, ITodoData data, HttpContext context)
    {
        try
        {
            var result = await data.GetEmployeeTasks(emp_Id);
            if (result?.Any() == false) return Results.NotFound();
            var totalTasks = result?.ToList().Count;
            context.Request.Headers.Add("Total_Tasks", totalTasks.ToString());
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetEmployeeTasksCount(string emp_Id, ITodoData data, HttpContext context)
    {
        try
        {
            var result = await data.GetEmployeeTasksCount(emp_Id);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
