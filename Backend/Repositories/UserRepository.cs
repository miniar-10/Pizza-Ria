using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddUserAsync(User user)
        {
            _appDbContext.Users.Add(user);  
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _appDbContext.Users.AnyAsync(u => u.Name == name);
        }

        public async  Task<User?> GetByNameAsync(string name)      
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
