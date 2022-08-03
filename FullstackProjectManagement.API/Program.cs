global using Microsoft.EntityFrameworkCore;
using FullstackProjectManagement.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ProjectManagementDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")!);
});


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();

app.Run();