using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User_Service.Data;
using User_Service.Repositories.Administration;
using User_Service.Services.Administration;
using Scalar.AspNetCore;
using Serilog;
using User_Service.Repositories.Employee;
using User_Service.Services.Auth;
using User_Service.Repositories.Driver;
using User_Service.Services.Driver;
using User_Service.Repositories.Worker;

var builder = WebApplication.CreateBuilder(args);

//logging(serilog)
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
//services
builder.Services.AddScoped<IAdministrationRepo,AdministrationRepo>();
builder.Services.AddScoped<IDriverRepo, DriverRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IWorkerRepo,WorkerRepo>();

builder.Services.AddScoped<IAdministrationService, AdministrationService>();
builder.Services.AddScoped<IDriverService, DriverService>();

builder.Services.AddScoped<IAuthService,AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200") // Angular dev server
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});




//add Context
builder.Services.AddDbContext<UserServiceContext>(options =>
  options.UseMySql(builder.Configuration.GetConnectionString("default"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("default")))
);

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
    };
});

builder.Services.AddControllers().AddNewtonsoftJson(); ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("AllowAngularDevClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
