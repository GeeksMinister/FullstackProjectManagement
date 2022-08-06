public class UserStoryData : IUserStoryData
{
    private readonly ISQLiteDataAccess _dbContext;

    public UserStoryData(ISQLiteDataAccess dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserStory>> GetAllUserStories()
    {
        string query = "SELECT Id, UserStoryName, Description, Priority, Status, ProjectId FROM UserStory";
        return await _dbContext.LoadData<UserStory, object>(query, new { });
    }

    public async Task<UserStory> GetUserStoryById(string Id)
    {
        string query = "SELECT Id, UserStoryName, Description, Priority, Status, ProjectId FROM UserStory WHERE Id = @Id";
        var result = await _dbContext.LoadData<UserStory, object>(query, new { Id });
        return result.FirstOrDefault()!;
    }

    public async Task DeleteUserStory(string Id)
    {
        string query = "DELETE FROM UserStory WHERE Id = @Id";
        await _dbContext.SaveData(query, new { Id });
    }

    public async Task UpdateUserStory(UserStory userStory)
    {
        string query = "UPDATE UserStory SET UserStoryName = @UserStoryName, Description = @Description, Priority = @Priority," +
                       " Status = @Status, ProjectId = @ProjectId WHERE Id = @Id";
        await _dbContext.SaveData(query, userStory);
    }

    public async Task<UserStory> InsertUserStory(UserStory userStory)
    {
        string query = "INSERT INTO UserStory (Id, UserStoryName, Description, Priority, Status, ProjectId)" +
                       "VALUES(@Id, @UserStoryName, @Description, @Priority, @Status, @ProjectId)";
        var result = await _dbContext.LoadData<UserStory, object>(query, userStory);
        return result.FirstOrDefault()!;
    }

}
