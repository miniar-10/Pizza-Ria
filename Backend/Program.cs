using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Backend.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddSingleton<DatabaseConfigService>();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/app_log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
Env.Load();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var configService = serviceProvider.GetRequiredService<DatabaseConfigService>();
    var connectionString = configService.GetConnectionString();

    options.UseNpgsql(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
