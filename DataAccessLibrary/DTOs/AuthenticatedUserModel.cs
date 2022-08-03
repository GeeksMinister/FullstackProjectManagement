namespace Projekt_Avancerad.NET.Models.DTOs;

public class AuthenticatedUserModel
{
    public string? UserName { get; set; }
    public int Age { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? Password { get; set; }
    public string? Access_Token { get; set; }
}
