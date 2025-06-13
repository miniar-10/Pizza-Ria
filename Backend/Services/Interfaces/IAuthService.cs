using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(LoginModel login);
        Task<bool> RegisterAsync(RegisterModel User);
        //Task <bool> ValidateToken(string token);
        //Task<string> RefreshToken(string token);


    }
}
