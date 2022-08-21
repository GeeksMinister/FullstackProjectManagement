namespace DataAccessLibrary.DTOs;

public class ProjectDto
{
    [StringLength(50)]
    public string ProjectName { get; set; } = string.Empty;
    [StringLength(1250, ErrorMessage = "1250 charactera max!")]
    public string? Description { get; set; }
    [StringLength(50)]
    public string Customer { get; set; } = string.Empty;
    [StringLength(12)]
    public string Status { get; set; } = "Not Started";

    public ProjectDto() { }

}
