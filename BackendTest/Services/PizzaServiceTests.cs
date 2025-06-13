using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.Services
{
    public class PizzaServiceTests
    {
        private readonly IPizzaRepository _fakeRepo;
        private readonly PizzaService _pizzaService;

        public PizzaServiceTests()
        {
            _fakeRepo = A.Fake<IPizzaRepository>();
            _pizzaService = new PizzaService(_fakeRepo);
        }

        [Fact]
        public async void GetAllAsync_ReturnsAllPizzas()
        {
            //Arrange

            var fakePizzas = new List<Pizza> {
                new Pizza { Id = 1, Name = "Margherita", IseGluteneFree = false },
                new Pizza { Id = 2, Name = "Pepperoni", IseGluteneFree = true }

            };
            A.CallTo(() => _fakeRepo.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Pizza>>(fakePizzas));


            //Act
            var result = await _pizzaService.getAllAsync();


            //Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                pizza => Assert.Equal("Margherita", pizza.Name),
                pizza => Assert.Equal("Pepperoni", pizza.Name));


        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectPizza()
        {
            //Arrange
            var pizza = new Pizza { Id = 2, Name = "Margherita" };
            A.CallTo(() => _fakeRepo.GetByIdAsync(2)).Returns(pizza);
            //Act
            var result = await _pizzaService.getByIdAsync(2);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Margherita", result?.Name);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsCorrectPizza()
        {
            //Arrange
            var pizza = new Pizza { Id = 2, Name = "Margherita" };
            A.CallTo(() => _fakeRepo.GetByNameAsync("Margherita")).Returns(pizza);
            //Act
            var result = await _pizzaService.getByNameAsync("Margherita");
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result?.Id);
        }
        [Fact]
        public async void AddAsync_ReturnsCreatedPizza()
        {
            //Arrange
            var pizzaToAdd = new Pizza { Name = "New Pizza" };
            A.CallTo(() => _fakeRepo.AddAsync(pizzaToAdd)).Returns(pizzaToAdd);

            //Act
            var result = await _pizzaService.AddAsync(pizzaToAdd);

            //Assert
            Assert.Equal("New Pizza", result.Name);


        }

        [Fact]
        public async void UpdateAsync_ReturnsTrue_WhenPizzaExists()
        {
            //Arrange
            var pizza = new Pizza { Id = 3, Name = "Updated" };

            A.CallTo(() => _fakeRepo.UpdateAsync(3, pizza)).Returns(true);

            //Act
            var result = await _pizzaService.UpdateAsync(3, pizza);

            //Assert
            Assert.True(result);


        }
        [Fact]
        public async void UpdateAsync_ReturnsFalse_WhenPizzaDesntExist()
        {
            //Arrange
            var pizza = new Pizza { Id = 99, Name = "Updated" };

            A.CallTo(() => _fakeRepo.UpdateAsync(99, pizza)).Returns(false);

            //Act
            var result = await _pizzaService.UpdateAsync(99, pizza);

            //Assert
            Assert.False(result);


        }
        [Fact]
        public async void DeleteAsync_ReturnsTrue_WhenPizzaExists()
        {
            //Arrange

            A.CallTo(() => _fakeRepo.DeleteAsync(3)).Returns(true);

            //Act
            var result = await _pizzaService.DeleteAsync(3);

            //Assert
            Assert.True(result);


        }
        [Fact]
        public async void DeleteAsync_ReturnsFalse_WhenPizzaDoesntExist()
        {
            //Arrange

            A.CallTo(() => _fakeRepo.DeleteAsync(99)).Returns(false);

            //Act
            var result = await _pizzaService.DeleteAsync(99);

            //Assert
            Assert.False(result);


        }
    }
}


