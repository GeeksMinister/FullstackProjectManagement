var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetValue<string>("AppSettings:SigningKey"))),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSingleton<ISQLiteDataAccess, SQLiteDataAccess>();
builder.Services.AddSingleton<IUserStoryData, UserStoryData>();
builder.Services.AddSingleton<IEmployeeData, EmployeeData>();
builder.Services.AddSingleton<IProjectData, ProjectData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ConfigureApi(app);

app.Run();


void ConfigureApi(WebApplication app)
{
    app.ConfigureApiEmployee();
    app.ConfigureApiProject();
    app.ConfigureApiUserStory();
}