using Microsoft.EntityFrameworkCore;
using Route_Service.Data;
using Route_Service.Reposetories.Route;
using Route_Service.Reposetories.Stop;
using Route_Service.Services.Route;
using Route_Service.Services.Stop;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.

builder.Services.AddScoped<IStopRepo, StopRepo>();
builder.Services.AddScoped<IRouteRepo, RouteRepo>();



builder.Services.AddScoped<IStopService, StopService>();
builder.Services.AddScoped<IRouteService, RouteService>();




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


builder.Services.AddDbContext<RouteServiceContext>(options =>
  options.UseMySql(builder.Configuration.GetConnectionString("default"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("default")))
);

builder.Services.AddControllers();
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
app.UseCors("AllowAngularDevClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
