using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class PizzaRepository: IPizzaRepository
    {
        private readonly AppDbContext _dbContext;

        public PizzaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pizza>> GetAllAsync()
        {
            return await _dbContext.Pizzas.ToListAsync();
        }

        public async Task<Pizza?> GetByIdAsync(int id)
        {
            return await _dbContext.Pizzas.FindAsync(id);
        }

        public async Task<Pizza?> GetByNameAsync(string name)
        {
            return await _dbContext.Pizzas.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Pizza> AddAsync(Pizza pizza)
        {
            _dbContext.Pizzas.Add(pizza);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<bool> UpdateAsync(int id, Pizza pizza)
        {
            var existing = await _dbContext.Pizzas.FindAsync(id);
            if (existing == null) return false;
            existing.Name = pizza.Name;
            existing.IseGluteneFree = pizza.IseGluteneFree;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _dbContext.Pizzas.FindAsync(id);
            if (existing == null) return false;
            _dbContext.Pizzas.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
