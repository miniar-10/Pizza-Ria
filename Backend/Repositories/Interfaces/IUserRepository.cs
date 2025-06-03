using Backend.Models;

namespace Backend.Repositories.Interfaces
{

        public interface IUserRepository
        {
            public Task<User?> GetByNameAsync(string name);
            public Task<bool> ExistsByNameAsync(string name);
            public Task AddUserAsync(User user);
        }



    }
