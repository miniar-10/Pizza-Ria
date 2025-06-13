using Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class DatabaseConfigService
    {
        private readonly IConfiguration _config;

        public DatabaseConfigService(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            var baseString = _config.GetConnectionString("DefaultConnection");
            Console.WriteLine("ENV VAR: " + Environment.GetEnvironmentVariable("DB_PASSWORD"));

            baseString = baseString?
                .Replace("{password}", Environment.GetEnvironmentVariable("DB_PASSWORD"));
            Console.WriteLine(baseString);


            return baseString ?? "";


        }
    }
}
