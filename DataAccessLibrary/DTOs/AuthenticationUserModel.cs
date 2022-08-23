﻿namespace DataAccessLibrary.DTOs;

public class AuthenticationUserModel
{
    public string GrantType { get; } = "Password";
    public string LoginInfo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
