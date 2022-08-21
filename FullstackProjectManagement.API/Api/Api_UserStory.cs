namespace FullstackProjectManagement.API.Api;

public static class Api_UserStory
{
    public static void ConfigureApiUserStory(this WebApplication app)
    {
        app.MapGet("/UserStory", GetAllUserStories);
        app.MapGet("/UserStory/{id}", GetUserStoryById);
        app.MapPut("/UserStory", UpdateUserStory);
        app.MapDelete("/UserStory", DeleteUserStory);
        app.MapPost("/UserStory", InsertUserStory);
    }

    private static async Task<IResult> GetAllUserStories(IUserStoryData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllUserStories();
            if (result?.Any() == false) return Results.NotFound();
            var totalUserStorys = result?.ToList().Count.ToString();
            context.Response.Headers.Add("Total_UserStorys", totalUserStorys);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUserStoryById(string id, IUserStoryData data)
    {
        try
        {
            var result = await data.GetUserStoryById(id);
            if (result is null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertUserStory(UserStoryDto userStoryDto, IUserStoryData data)
    {
        try
        {
            if (userStoryDto == null) return Results.BadRequest();
            var userStory = new UserStory();
            userStory.UserStoryName = userStoryDto.UserStoryName;
            userStory.Description = userStoryDto.Description;
            userStory.Priority = userStoryDto.Priority;
            userStory.Status = userStoryDto.Status;
            userStory.ProjectId = userStoryDto.ProjectId;
            await data.InsertUserStory(userStory);
            return Results.Ok(userStory);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUserStory(string id, IUserStoryData data)
    {
        try
        {
            var userStory = await data.GetUserStoryById(id);
            if (userStory == null) return Results.NotFound();
            await data.DeleteUserStory(id);
            return Results.Ok($"UserStory with Id: {id} Was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> UpdateUserStory(string id, UserStoryDto userStoryDto, IUserStoryData data)
    {
        try
        {
            var userStory = await data.GetUserStoryById(id);
            if (userStory == null) return Results.NotFound();
            userStory.UserStoryName = userStoryDto.UserStoryName;
            userStory.Description = userStoryDto.Description;
            userStory.Priority = userStoryDto.Priority;
            userStory.Status = userStoryDto.Status;
            userStory.ProjectId = userStoryDto.ProjectId;
            await data.UpdateUserStory(userStory);
            return Results.Ok(userStory);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
