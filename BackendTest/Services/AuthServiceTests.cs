using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.Services
{
    public class AuthServiceTests
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _passwordHasher = A.Fake<IPasswordHasher<User>>();   
            _userRepository = A.Fake<IUserRepository>();
            _authService = new AuthService(_passwordHasher,_userRepository);
        }

        [Fact]
        public async void RegistrateAsync_ReturnsFalse_WhenUserExists()
        {
            //Arrange
            var fake_user = new RegisterModel { Name = "Admin", HashedPassword = "hasedadmin", Address = "secret adress", PhoneNumber = 55432432 };
            A.CallTo(()=>_userRepository.ExistsByNameAsync("Admin")).Returns(true);

            //Act
            var result =await _authService.RegisterAsync(fake_user);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void RegistrateAsync_ReturnsTrue_WhenSucess()
        {
            //Arrange
            var fake_user = new RegisterModel { Name = "Admin", HashedPassword = "hasedadmin", Address = "secret adress", PhoneNumber = 55432432 };
            A.CallTo(() => _userRepository.ExistsByNameAsync("Admin")).Returns(false);

            //Act
            var result = await _authService.RegisterAsync(fake_user);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public async void RegistrateAsync_AddsUserToRepo()
        {
            var fake_user = new RegisterModel { Name = "Admin", HashedPassword = "hasedadmin", Address = "secret adress", PhoneNumber = 55432432 };
            var result = await _authService.RegisterAsync(fake_user);

            A.CallTo(()=>_userRepository.AddUserAsync(A<User>._))
    .MustHaveHappenedOnceExactly();

            Assert.True(result);
        }
    }
}
