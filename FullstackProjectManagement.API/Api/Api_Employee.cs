public static class Api_Employee
{
    public static void ConfigureApiEmployee(this WebApplication app)
    {
        app.MapGet("/Employee", GetAllEmployees);
        app.MapPost("/Login", Login);
        app.MapGet("/Employee/{id}", GetEmployeeById);
        app.MapPost("/Employee", InsertEmployee);
        app.MapPut("Employee", UpdateEmployee);
        app.MapDelete("/Employee", DeleteEmployee);
    }

    private static async Task<IResult> Login(string loginInfo, string password, IConfiguration config, IEmployeeData data, HttpContext http)
    {
        try
        {
            var employee = await data.GetEmployeeByEmailOrId(loginInfo);
            if (employee == null) return Results.NotFound("Login Info Wasn't Found");

            (var passwordSalt, var passwordHash) = await GetPasswordAndSaltHash(loginInfo, data);
            if (VerifyPassword(password, passwordSalt, passwordHash) == false)
            {
                return Results.BadRequest("Wrong Password");
            }
            var authenticatedUser = new AuthenticatedUserModel
            {
                UserName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Access_Token = CreateToken(employee, config)
            };
            SetRefreshToken(new RefreshToken(), http);
            return Results.Ok(authenticatedUser);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static void SetRefreshToken(/*Employee employee, */RefreshToken refreshToken, HttpContext http)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        http.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        //employee.RefreshToken = refreshToken.Token;
        //employee.TokenCreated = refreshToken.Created;
        //employee.TokenExpires = refreshToken.Expires;
    }

    //[Authorize(Roles = "Construction Manager, Estimator")]
    private static async Task<IResult> GetAllEmployees(IEmployeeData data, HttpContext context)
    {
        try
        {
            var result = await data.GetAllEmployees();
            if (result?.Any() == false) return Results.NotFound("No Employee Was Retrieved From Database");
            var totalEmployees = result?.ToList().Count;
            context.Response.Headers.Add("Total_Employees", totalEmployees.ToString());
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetEmployeeById(string id, IEmployeeData data)
    {
        try
        {
            var result = await data.GetEmployeeById(id);
            if(result is null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertEmployee(EmployeeDto employeeDto, IEmployeeData data, IMapper mapper)
    {
        try
        {
            if(employeeDto == null) return Results.BadRequest();
            var employee = mapper.Map<Employee>(employeeDto);
            (employee.PasswordSalt, employee.PasswordHash) = CreatePasswordHash(employeeDto.Password);
            await data.InsertEmployee(employee);
            return Results.Ok(employee);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateEmployee(string id, EmployeeDto employeeDto, IEmployeeData data, IMapper mapper)
    {
        try
        {
            var employee = await data.GetEmployeeById(id);
            if (employee == null) return Results.NotFound();
            employee = mapper.Map<Employee>(employeeDto);
            employee.Id = id;
            (employee.PasswordSalt, employee.PasswordHash) = CreatePasswordHash(employeeDto.Password);
            await data.UpdateEmployee(employee);
            return Results.Ok(employee);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteEmployee(string id, IEmployeeData data)
    {
        try
        {
            var employee = await data.GetEmployeeById(id);
            if(employee == null) return Results.NotFound();
            await data.DeleteEmployee(id);
            return Results.Ok($"Employee with Id: {id} Was successfully removed");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static (byte[] passwordSalt, byte[] passwordHash) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        return (passwordSalt: hmac.Key, passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private static async Task<(byte[] passwordSalt, byte[] passwordHash)> GetPasswordAndSaltHash(
                                                            string loginInfo, IEmployeeData data)
    {
        var hexValues = await data.GetPasswordAndSaltHex(loginInfo);
        string saltHex = hexValues.Split("_")[0];
        string passwordHex = hexValues.Split("_")[1];
        return (passwordSalt: Convert.FromHexString(saltHex), passwordHash: Convert.FromHexString(passwordHex));
    }

    private static string CreateToken(Employee employee, IConfiguration config)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, employee.Username()),
            new Claim(ClaimTypes.SerialNumber, employee.Id),
            new Claim(ClaimTypes.Role, employee.Role),
            new Claim(ClaimTypes.Email, employee.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            config.GetValue<string>("AppSettings:SigningKey")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds,
            expires: DateTime.Now.AddDays(1));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
