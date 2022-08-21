using FullstackProjectManagement.WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(typeof(MapperInitializer));

var apiLocation = builder.Configuration["ApiLocation"]!;
builder.Services.AddRefitClient<ICurrencyClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IEmployeeClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IProjectClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<ITodoClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
