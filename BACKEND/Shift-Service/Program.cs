using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Shift_Service.Data;
using Shift_Service.Repositories.Group;
using Shift_Service.Repositories.Shift;
using Shift_Service.Services.Group;
using Shift_Service.Services.Shift;
using Shift_Service.Services.ShiftRotation;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
builder.Services.AddScoped<IShiftRepo, ShiftRepo>();
builder.Services.AddScoped<IGroupRepo, GroupRepo>();

builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddHostedService<ShiftRotationService>();



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

builder.Services.AddDbContext<ShiftServiceContext>(options =>
  options.UseMySql(builder.Configuration.GetConnectionString("default"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("default")))
);
builder.Services.AddDbContextFactory<ShiftServiceContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("default"))),ServiceLifetime.Scoped);



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
