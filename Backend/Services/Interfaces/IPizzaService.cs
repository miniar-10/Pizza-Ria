using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IPizzaService
    {
        Task<IEnumerable<Pizza>> getAllAsync();
        Task <Pizza?> getByIdAsync (int id);
        Task <Pizza?> getByNameAsync (string name);
        Task <Pizza> AddAsync (Pizza pizza);
        Task <bool>DeleteAsync (int id);
        Task<bool> UpdateAsync(int id, Pizza pizza);

    }
}
