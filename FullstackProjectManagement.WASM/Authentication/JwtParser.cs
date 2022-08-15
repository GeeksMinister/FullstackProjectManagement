namespace FullstackProjectManagement.WASM.Authentication;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        ExctractRolesFromJwt(claims, keyValuePairs!);
        claims.AddRange(keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

        return claims;
    }

    private static void ExctractRolesFromJwt(List<Claim> claims, Dictionary<string, object> keyValuePairs)
    {
        keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);

        if (roles is null) return;

        var parsedRoles = roles?.ToString()?.Split(',');

        foreach (var role in parsedRoles!)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
        }
        
        keyValuePairs.Remove(ClaimTypes.Role);

    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
