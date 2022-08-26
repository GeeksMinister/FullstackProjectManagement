namespace FullstackProjectManagement.API.Api;

public static class Api_Todo
{
    public static void ConfigureApi_EmployeeTasks(this WebApplication app)
    {
        app.MapGet("/Task", GetAllTasks);
        app.MapGet("/Task/{id}", GetTaskById);
        app.MapGet("/Task/Employee/{EmployeeId}", GetEmployeeTasks);
        app.MapGet("/Task/Employee/Count/{EmployeeId}", GetEmployeeTasksCount);
        app.MapPost("/Task", InsertTask);
        app.MapPut("/Task/{id}", UpdateTask);
        app.MapDelete("/Task/{id}", DeleteTask);
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

    private static async Task<IResult> GetTaskById(string id, ITodoData data)
    {
        try
        {
            var result = await data.GetTaskById(id);
            if (result == null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetEmployeeTasks(string employeeId, ITodoData data, HttpContext context)
    {
        try
        {
            var result = await data.GetEmployeeTasks(employeeId);
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

    private static async Task<IResult> GetEmployeeTasksCount(string employeeId, ITodoData data, HttpContext context)
    {
        try
        {
            var result = await data.GetEmployeeTasksCount(employeeId);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertTask(TodoDto todoDto, ITodoData data, IMapper mapper)
    {
        try
        {
            if (todoDto == null) return Results.BadRequest();
            var todo = mapper.Map<Todo>(todoDto);
            await data.InsertTask(todo);
            return Results.Ok(todo);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteTask(string id, ITodoData data)
    {
        try
        {
            var todo = await data.GetTaskById(id);
            if (todo == null) return Results.NotFound();
            await data.DeleteTask(id);
            return Results.Ok($"Task with Id: {id} Was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> UpdateTask(string id, TodoDto todoDto, ITodoData data, IMapper mapper)
    {
        try
        {
            var todo = await data.GetTaskById(id);
            if (todo == null) return Results.NotFound();
            todo = mapper.Map<Todo>(todoDto);
            todo.Id = id;
            await data.UpdateTask(todo);
            return Results.Ok(todo);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


}
