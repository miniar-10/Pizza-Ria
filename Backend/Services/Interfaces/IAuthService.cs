using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginModel login);
        Task<bool> RegisterAsync(RegisterModel User);
        //Task <bool> ValidateToken(string token);
        //Task<string> RefreshToken(string token);


    }
}
