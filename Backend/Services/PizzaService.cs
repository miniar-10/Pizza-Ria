using Backend.Data;
using Backend.Migrations;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PizzaService : IPizzaService
    {
        readonly IPizzaRepository _pizzarRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzarRepository = pizzaRepository;
        }

        public async Task<IEnumerable<Pizza>> getAllAsync()
        {
            return await _pizzarRepository.GetAllAsync();
        }
        public async Task<Pizza?> getByIdAsync(int id)
        {
            return await _pizzarRepository.GetByIdAsync(id);
        }
        public async Task<Pizza?> getByNameAsync(string name)
        {
            return await _pizzarRepository.GetByNameAsync(name);

        }
        public async Task<Pizza> AddAsync(Pizza pizza)
        {
            return await _pizzarRepository.AddAsync(pizza);
        }

        public async Task<bool> UpdateAsync(int id, Pizza pizza)
        {
            return await _pizzarRepository.UpdateAsync(id, pizza);

        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _pizzarRepository.DeleteAsync(id);
        }




    }
}
