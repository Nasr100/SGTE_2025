using MassTransit;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Trip_Service.Data;
using Trip_Service.Repositories.Bus;
using Trip_Service.Repositories.MiniTrip;
using Trip_Service.Repositories.Trip;
using Trip_Service.Services.Minitrip;
using Trip_Service.Services.Trip;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
builder.Services.AddScoped<IMinitripRepo, MiniTripRepo>();
builder.Services.AddScoped<ITripRepo, TripRepo>();
builder.Services.AddScoped<IBusRepo, BusRepo>();

builder.Services.AddScoped<IMinitripService,MinitripService>();
builder.Services.AddScoped<ITripService, TripService>();




builder.Services.AddMassTransit(x =>
{
x.UsingRabbitMq((context, cfg) =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ConfigureEndpoints(context);
});
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

builder.Services.AddDbContext<TripServiceContext>(options =>
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
