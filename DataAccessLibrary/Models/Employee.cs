﻿public class Employee
{
    [StringLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    [StringLength(10)]
    public string Birthdate { get; set; } = DateTime.Now.AddYears(-18).ToShortDateString();
    [StringLength(50)]
    [Phone]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    public string Phone { get; set; } = string.Empty;
    [EmailAddress]
    [StringLength(300)]
    public string Email { get; set; } = string.Empty;
    [StringLength(20)]
    public string? City { get; set; }
    public string Role { get; set; } = "Employee";
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? PasswordHash { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? PasswordSalt { get; set; }
    [StringLength(512)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? TokenCreated { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? TokenExpires { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Todo>? Todos { get; set; }

    public Employee() { }

    public string Username() => FirstName + " " + LastName;
}
