using EmployeeManagementApi.Middlewares;
using EmployeeManagementApi.Repository.Interfaces;
using EmployeeManagementApi.Repository;
using EmployeeManagementApi.Services;
using EmployeeManagementApi.Services.Interfaces;
using EmployeeManagementApi.Mappings;
using EmployeeManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(AutomapperProfile));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)
.UseSeeding((context, _) =>
{
    var employees = SeedData.Get();

    var existingIds = context.Set<Employee>()
        .Select(e => e.Id)
        .ToHashSet();

    var missingEmployees = employees
        .Where(e => !existingIds.Contains(e.Id))
        .ToList();

    if (missingEmployees.Count > 0)
    {
        context.Set<Employee>().AddRange(missingEmployees);
        context.SaveChanges();
    }

    var roles = new[]
        {
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            }
        };

    var existingRoles = context.Set<IdentityRole>()
        .Select(r => r.Name)
        .ToHashSet();

    var missingRoles = roles
            .Where(r => !existingRoles.Contains(r.Name))
            .ToList();

    if (missingRoles.Count > 0)
    {
        context.Set<IdentityRole>().AddRange(missingRoles);
        context.SaveChanges();
    }
})
);

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts => opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
            )
    });

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        var canConnect = await dbContext.Database.CanConnectAsync();

        Console.WriteLine(canConnect
            ? "Database connection successful!"
            : "Database connection failed!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
