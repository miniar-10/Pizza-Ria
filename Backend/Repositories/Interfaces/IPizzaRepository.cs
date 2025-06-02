using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetAllAsync();
        Task<Pizza?> GetByIdAsync(int id);
        Task<Pizza?> GetByNameAsync(string name);
        Task<Pizza> AddAsync(Pizza pizza);
        Task<bool> UpdateAsync(int id, Pizza pizza);
        Task<bool> DeleteAsync(int id);
    }
}
