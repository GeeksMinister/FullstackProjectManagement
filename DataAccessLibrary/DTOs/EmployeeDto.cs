public class EmployeeDto
{
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    [StringLength(10)]
    public string Birthdate { get; set; } = DateTime.Now.ToShortDateString();
    [StringLength(50)]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    public string Phone { get; set; } = string.Empty;
    [EmailAddress]
    [StringLength(300)]
    public string Email { get; set; } = string.Empty;
    [StringLength(20)]
    public string? City { get; set; }
    public string Role { get; set; } = "Employee";

    public EmployeeDto() { }
}
