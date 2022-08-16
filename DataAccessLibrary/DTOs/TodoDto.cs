public class TodoDto
{
    [Required]
    [DisplayName("Name")]
    [StringLength(100, ErrorMessage = "Name is too big. 100 charactera max!")]
    public string TaskName { get; set; } = string.Empty;

    [StringLength(1250, ErrorMessage = "1250 charactera max!")]
    public string? Description { get; set; }

    [Required]
    [Range(1, 5)]
    [DisplayName("Set Priority")]
    public int? Priority { get; set; } = null;

    [Required]
    public string Status { get; set; } = "Not Started";

    [StringLength(36)]
    public string EmployeeId { get; set; } = string.Empty;

    [StringLength(36)]
    public string? ProjectId { get; set; }

    public TodoDto()
    {

    }

}