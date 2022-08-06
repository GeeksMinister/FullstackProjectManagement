namespace DataAccessLibrary.DTOs;

public class AuthenticatedUserModel
{
    public string UserName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? Access_Token { get; set; }
}
