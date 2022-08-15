using Newtonsoft.Json;

namespace FullstackProjectManagement.WASM.Authentication;

public class AuthenticationService : IAuthenticationService
{
	private readonly AuthenticationStateProvider _stateProvider;
	private readonly IConfiguration _config;
	private readonly ILocalStorageService _localStorage;
	private readonly HttpClient _client;

	public AuthenticationService(AuthenticationStateProvider stateProvider,
							  IConfiguration config,
							  ILocalStorageService localStorage,
							  HttpClient client)
	{
		_stateProvider = stateProvider;
		_config = config;
		_localStorage = localStorage;
		_client = client;
	}

	public async Task<AuthenticatedUserModel?> Login(AuthenticationUserModel user)
	{
		var jsonValues = new Dictionary<string, string>()
		{
			{"loginInfo", user.LoginInfo }, {"password", user.Password}
		};
		var jsonString = JsonConvert.SerializeObject(jsonValues);
		StringContent httpContentData = new StringContent(jsonString, Encoding.UTF8, "application/json");

		string location = _config["ApiLocation"]!;
		var authResult = await _client.PostAsync(location, httpContentData);
		var resultContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode == false)
		{
			return null;
		}

		var authenticatedUser = JsonConvert.DeserializeObject<AuthenticatedUserModel>(resultContent);
		await _localStorage.SetItemAsync(_config["authTokenStorageKey"], authenticatedUser!.Access_Token);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUser.Access_Token);
		((AuthStateProvider)_stateProvider).NotifyUserAuthentication(authenticatedUser.Access_Token!);

		return authenticatedUser;
	}

	public async Task LogOut()
	{
		await _localStorage.RemoveItemAsync(_config["authTokenStorageKey"]);
		_client.DefaultRequestHeaders.Authorization = null;
		((AuthStateProvider)_stateProvider).NotifyUserLogout();
	}
}
