using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppAuthorization;

namespace AppAuthorizationTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private AuthService _authService;

        [TestInitialize]
        public void Setup()
        {
            _authService = new AuthService();
        }

        [TestMethod]
        public void AuthenticateUser_WithEmptyCredentials_ReturnsFalse()
        {
            string username = "";
            string password = "";

            bool result = _authService.AuthenticateUser(username, password);

            Assert.IsFalse(result, "Пустые учетные данные должны возвращать false");
        }

        [TestMethod]
        public void AuthenticateUser_WithNonExistentUser_ReturnsFalse()
        {
            string username = "nonexistentuser";
            string password = "Password123!";

            bool result = _authService.AuthenticateUser(username, password);

            Assert.IsFalse(result, "Несуществующий пользователь должен возвращать false");
        }

        [TestMethod]
        public void AuthenticateUser_WithIncorrectPassword_ReturnsFalse()
        {
            string username = "testuser";
            string password = "WrongPassword123!";

            bool result = _authService.AuthenticateUser(username, password);

            Assert.IsFalse(result, "Неверный пароль должен возвращать false");
        }

        [TestMethod]
        public void AuthenticateUser_WithValidCredentials_ReturnsTrue()
        {
            string username = "Dispatcher@";
            string password = "Dispatcher@";

            bool result = _authService.AuthenticateUser(username, password);

            Assert.IsTrue(result, "Правильные учетные данные должны возвращать true");
        }
    }
}