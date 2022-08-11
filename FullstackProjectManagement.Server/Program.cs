var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddRefitClient<ICurrencyClientData>().ConfigureHttpClient(
    client =>client.BaseAddress = new Uri("https://localhost:7128"));

builder.Services.AddRefitClient<IEmployeeClientData>().ConfigureHttpClient(
    client =>client.BaseAddress = new Uri("https://localhost:7128"));


builder.Services.AddSingleton<ISQLiteDataAccess, SQLiteDataAccess>();
builder.Services.AddSingleton<ICurrencyData, CurrencyData>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
