using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ECommerce.Features;
using ECommerce.Persistence;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.local.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register the DbContextOptions<DataContext>
builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("PostgresDsn"));
});

// Other service registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();