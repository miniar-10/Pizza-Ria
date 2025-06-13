using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Services;
using Backend.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove previous DbContext registration
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                // Remove previous DatabaseConfigService registration
                var configServiceDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DatabaseConfigService));
                if (configServiceDescriptor != null)
                    services.Remove(configServiceDescriptor);

                // Setup in-memory configuration for the test
                var configDict = new Dictionary<string, string>
        {
            { "ConnectionStrings:DefaultConnection", "Host=localhost;Database=TestDb;Username=test;Password={password}" }
        };
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(configDict)
                    .Build();

                Environment.SetEnvironmentVariable("DB_PASSWORD", "testpass");

                // Register IConfiguration singleton
                services.AddSingleton<IConfiguration>(configuration);

                // Register DatabaseConfigService as Scoped or Transient to match usual DI usage
                services.AddScoped<DatabaseConfigService>();

                // Register DbContext to use InMemory
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Build service provider and ensure DB is created
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            });
        }




    }
}
