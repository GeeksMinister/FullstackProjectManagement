public class UserStoryDto
{
    [Required]
    [DisplayName("Name")]
    [StringLength(100, ErrorMessage = "Name is too big. 100 charactera max!")]
    public string UserStoryName { get; set; } = string.Empty;
    [StringLength(1250, ErrorMessage = "1250 charactera max!")]
    public string? Description { get; set; }
    [Range(1, 5)]
    [DisplayName("Set Priority")]
    public int? Priority { get; set; } = null;
    [StringLength(12)]
    public string Status { get; set; } = "Not Started";

    [StringLength(36)]
    public string ProjectId { get; set; } = string.Empty;
}
