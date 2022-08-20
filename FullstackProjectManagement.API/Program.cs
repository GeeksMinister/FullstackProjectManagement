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
            .GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SigningKey"))),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<ISQLiteDataAccess, SQLiteDataAccess>();
builder.Services.AddSingleton<IUserStoryData, UserStoryData>();
builder.Services.AddSingleton<IEmployeeData, EmployeeData>();
builder.Services.AddSingleton<ICurrencyData, CurrencyData>();
builder.Services.AddSingleton<IProjectData, ProjectData>();
builder.Services.AddSingleton<ITodoData, TodoData>();
builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.ConfigureApiProject();
app.ConfigureApiEmployee();
app.ConfigureApiUserStory();
app.ConfigureApiEmployeeTasks();

app.UseCors("OpenCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.Run();
