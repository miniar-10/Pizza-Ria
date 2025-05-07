namespace Backend.Data
{
    // Data/AppDbContext.cs
    using Microsoft.EntityFrameworkCore;
    using Backend.Models; 

    namespace YourProjectName.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Pizza> Pizzas { get; set; }

        }
    }
}
