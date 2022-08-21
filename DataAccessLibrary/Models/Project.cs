namespace DataAccessLibrary.Models;

public class Project
{
    [StringLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [StringLength(50)]
    public string ProjectName { get; set; } = string.Empty;
    [StringLength(1250, ErrorMessage = "1250 charactera max!")]
    public string? Description { get; set; }
    [StringLength(50)]
    public string Customer { get; set; } = string.Empty;
    [StringLength(12)]
    public string Status { get; set; } = "Not Started";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<UserStory>? UserStories { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Todo>? Todos { get; set; }

    public Project() { }

}
