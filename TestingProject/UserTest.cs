using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.DTO.Responses.Animals;
using FinalWorkshop.EndPoints.Animals;
using FinalWorkshop.Model;
using FinalWorkshop.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestingProject
{
    [TestClass]
    public class UserTest
    {
        private DatabaseContext _context;
        private UserRepository _userRepository;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DatabaseContext(options);
            _userRepository = new UserRepository(_context);

            _context.Users.AddRange(new List<User>
            {
                new User { Id = 1, Mail = "testmailun@gmail.com", Password = "password1" },
                new User { Id = 2, Mail = "testmaildeux@gmail.com", Password = "password2" }
            });
            _context.SaveChanges();
        }

        //On réinitialise la DB simulée à chaque fois qu'on lance les tests
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var result = await _userRepository.GetAllAsync();
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectUser()
        {
            var result = await _userRepository.GetByIdAsync(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public async Task AddAsync_AddsUser()
        {
            var newUser = new User { Id = 3, Mail = "testmailtrois@gmail.com", Password = "password3" };
            await _userRepository.AddAsync(newUser);
            var result = await _userRepository.GetByIdAsync(3);
            Assert.IsNotNull(result);
            Assert.AreEqual("testmailtrois@gmail.com", result.Mail);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesUser()
        {
            var existingUser = await _userRepository.GetByIdAsync(1);
            existingUser.Mail = "updatedmail@gmail.com";
            await _userRepository.UpdateAsync(existingUser);
            var result = await _userRepository.GetByIdAsync(1);
            Assert.AreEqual("updatedmail@gmail.com", result.Mail);
        }

        [TestMethod]
        public async Task DeleteAsync_DeletesUser()
        {
            await _userRepository.DeleteAsync(1);
            var result = await _userRepository.GetByIdAsync(1);
            Assert.IsNull(result);
        }
    }
}