using Backend.Data;
using Backend.Migrations;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PizzaService: IPizzaService
    {
        readonly AppDbContext _dbContext;

        public PizzaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pizza>> getAllAsync()
        {
            return await _dbContext.Pizzas.ToListAsync();
        }
        public async Task <Pizza?> getByIdAsync(int id)
        {
            return await _dbContext.Pizzas.FindAsync(id);
        }
        public async Task<Pizza?> getByNameAsync(string name)
        {
            return await _dbContext.Pizzas.FirstOrDefaultAsync((pizza) => pizza.Name == name);

        }
        public async Task <Pizza> AddAsync(Pizza pizza)
        {
             _dbContext.Add(pizza);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task <bool> UpdateAsync(int id, Pizza pizza)
        {
            var existing=await _dbContext.Pizzas.FindAsync(id);
            if(existing==null) return false;
            existing.Name = pizza.Name;
            existing.IseGluteneFree=pizza.IseGluteneFree;
            await _dbContext.SaveChangesAsync();
            return true;

        }
        public async Task <bool> DeleteAsync(int id)
        {
            var existing = await _dbContext.Pizzas.FindAsync(id);
            if(existing==null) return false;
            _dbContext.Pizzas.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }




    }
}
