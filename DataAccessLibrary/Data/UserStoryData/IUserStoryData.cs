
public interface IUserStoryData
{
    Task DeleteUserStory(string Id);
    Task<IEnumerable<UserStory>> GetAllUserStories();
    Task<UserStory> GetUserStoryById(string Id);
    Task<UserStory> InsertUserStory(UserStory UserStory);
    Task UpdateUserStory(UserStory UserStory);
}