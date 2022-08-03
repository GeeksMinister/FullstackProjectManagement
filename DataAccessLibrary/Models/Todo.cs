public class Todo
{
    [StringLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    [DisplayName("Name")]
    [StringLength(100, ErrorMessage = "Name is too big. 100 charactera max!")]
    public string TaskName { get; set; } = string.Empty;
    [StringLength(1250, ErrorMessage = "1250 charactera max!")]
    public string? Description { get; set; }
    [Range(1, 5)]
    [DisplayName("Set Priority")]
    public int? Priority { get; set; } = null;
    public string Status { get; set; } = "Not Started";

    public Employee Employee { get; set; } = new Employee();
    [StringLength(36)]
    public string EmployeeId { get; set; } = string.Empty;
    //Nullable since a task doesn't always need to be attached to a project
    public Project? Project { get; set; }
    [StringLength(36)]
    public string? ProjectId { get; set; }

    public Todo()
    {

    }

}

