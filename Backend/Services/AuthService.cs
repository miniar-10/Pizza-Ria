using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
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

        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public AuthService(IPasswordHasher<User> password, IUserRepository userRepository)
        {
            _passwordHasher = password;
            _userRepository = userRepository;

        }

        public async Task<string> AuthenticateAsync(LoginModel login)
        {
            var userExists = await _userRepository.ExistsByNameAsync(login.LoginName);
            if (!userExists)
            {
                return null;
            }
            var user = await _userRepository.GetByNameAsync(login.LoginName);
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
            var userExists = await _userRepository.ExistsByNameAsync(User.Name);

            if (userExists)
                return false;

            var user = new User
            {
                Name = User.Name,
                Address = User.Address,
                PhoneNumber = User.PhoneNumber,
            };

            // Hash the password and store it
            user.HashedPassword = _passwordHasher.HashPassword(user, User.HashedPassword);

            await _userRepository.AddUserAsync(user);

            return true;
        }

    }

}
