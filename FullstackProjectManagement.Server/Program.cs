var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var apiLocation = builder.Configuration["ApiLocation"]!;
builder.Services.AddRefitClient<ICurrencyClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IEmployeeClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IProjectClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<ITodoClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));

builder.Services.AddAutoMapper(typeof(MapperInitializer));

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
