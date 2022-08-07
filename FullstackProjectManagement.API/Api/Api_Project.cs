public static class Api_Project
{
    public static void ConfigureApiProject(this WebApplication app)
    {
        app.MapGet("/Project{id}", GetProjectById);
        app.MapGet("/Project", GetAllProjects);
        app.MapPost("/Project", InsertProject);
        app.MapPut("/Project", UpdateProject);
        app.MapDelete("/Project", DeleteProject);
    }

    private static async Task<IResult> GetAllProjects(IProjectData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllProjects();
            if (result?.Any() == false) return Results.NotFound();
            var totalProjects = result?.ToList().Count.ToString();
            context.Response.Headers.Add("Total_Projects", totalProjects);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetProjectById(string id, IProjectData data)
    {
        try
        {
            var result = await data.GetProjectById(id);
            if (result is null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertProject(ProjectDto projectDto, IProjectData data)
    {
        try
        {
            if (projectDto == null) return Results.BadRequest();
            var project = new Project();
            project.ProjectName = projectDto.ProjectName;
            project.Description = projectDto.Description;
            project.Customer = projectDto.Customer;
            project.Status = projectDto.Status;
            await data.InsertProject(project);
            return Results.Ok(project);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteProject(string id, IProjectData data)
    {
        try
        {
            var project = await data.GetProjectById(id);
            if (project == null) return Results.NotFound();
            await data.DeleteProject(id);
            return Results.Ok($"Project with Id: {id} Was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> UpdateProject(string id, ProjectDto projectDto, IProjectData data)
    {
        try
        {
            var project = await data.GetProjectById(id);
            if (project == null) return Results.NotFound();
            project.ProjectName = projectDto.ProjectName;
            project.Description = projectDto.Description;
            project.Customer = projectDto.Customer;
            project.Status = projectDto.Status;
            await data.UpdateProject(project);
            return Results.Ok(project);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


}
