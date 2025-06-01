using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AppDbContext _dbContext;

        public AuthService(IConfiguration configuration, IPasswordHasher<User> password, AppDbContext dbContext)
        {
            _passwordHasher = password;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<string> AuthenticateAsync(LoginModel login)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Name == login.LoginName);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, login.Password);
            if (result != PasswordVerificationResult.Success)
                return null;


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is a Super Secure password 1234"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, login.LoginName)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<bool> RegisterAsync(RegisterModel User)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Name == User.Name))
                return false; 

            var user = new User
            {
                Name = User.Name,
                Address = User.Address,
                PhoneNumber = User.PhoneNumber,
            };

            // Hash the password and store it
            user.HashedPassword = _passwordHasher.HashPassword(user, User.HashedPassword);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }

}
