public class AuthenticatedUserModel
{
    public string UserName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Access_Token { get; set; }

}
