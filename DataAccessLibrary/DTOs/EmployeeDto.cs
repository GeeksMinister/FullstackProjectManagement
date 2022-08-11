﻿public class EmployeeDto
{
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [FromNow]
    public DateTime Birthdate { get; set; }
    
    [StringLength(20)]
    public string? City { get; set; }

    public string Role { get; set; } = "Employee";
    
    [StringLength(50)]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    [Phone]
    public string Phone { get; set; } = string.Empty;
    
    [EmailAddress]
    [StringLength(300)]
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    public EmployeeDto() { }
}
