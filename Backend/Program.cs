using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPizzaService, PizzaService>();
// Register the config service
builder.Services.AddSingleton<DatabaseConfigService>();
Env.Load();

// Then use it to configure DbContext
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var configService = serviceProvider.GetRequiredService<DatabaseConfigService>();
    var connectionString = configService.GetConnectionString();

    options.UseNpgsql(connectionString);
});

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
