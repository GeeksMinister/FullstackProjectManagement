namespace FullstackProjectManagement.WASM.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly IConfiguration _config;
	private readonly AuthenticationState _anonymous;

	public AuthStateProvider(HttpClient client,
						  ILocalStorageService localStorage,
						  IConfiguration config)
	{
		_client = client;
		_localStorage = localStorage;
		_config = config;
		_anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
	}

	public void NotifyUserLogout()
	{
		NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
	}

	public void NotifyUserAuthentication(string token)
	{
		var authState = new AuthenticationState(new ClaimsPrincipal(
			 new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
		NotifyAuthenticationStateChanged(Task.FromResult(authState));
	}

    // Unused Abstract memeber
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var token = await _localStorage.GetItemAsync<string>(_config["authTokenStorageKey"]);
		if (string.IsNullOrWhiteSpace(token))
		{
			return _anonymous;
		}
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
		return new AuthenticationState(new ClaimsPrincipal( new ClaimsIdentity(
			JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
	}
}
