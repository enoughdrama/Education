using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppAuthorization;

namespace AppAuthorizationTests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private RegistrationService _registrationService;

        [TestInitialize]
        public void Setup()
        {
            _registrationService = new RegistrationService();
        }

        [TestMethod]
        public void RegisterUser_WithEmptyFields_ReturnsFalse()
        {
            string username = "";
            string password = "";
            string confirmPassword = "";

            var result = _registrationService.RegisterUser(username, password, confirmPassword);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Заполните все поля.", result.ErrorMessage);
        }

        [TestMethod]
        public void RegisterUser_WithNonMatchingPasswords_ReturnsFalse()
        {
            string username = "newuser";
            string password = "Passwordasd!";
            string confirmPassword = "DifferentPasswordasd!";

            var result = _registrationService.RegisterUser(username, password, confirmPassword);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Пароли не совпадают.", result.ErrorMessage);
        }

        [TestMethod]
        public void RegisterUser_WithShortPassword_ReturnsFalse()
        {
            string username = "newuser";
            string password = "Pass1!";
            string confirmPassword = "Pass1!";

            var result = _registrationService.RegisterUser(username, password, confirmPassword);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Пароль должен содержать не менее 8 символов.", result.ErrorMessage);
        }

        [TestMethod]
        public void RegisterUser_WithoutSpecialCharacters_ReturnsFalse()
        {
            string username = "newuser";
            string password = "Passwordasd";
            string confirmPassword = "Passwordasd";

            var result = _registrationService.RegisterUser(username, password, confirmPassword);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Пароль должен содержать хотя бы один спецсимвол.", result.ErrorMessage);
        }

        [TestMethod]
        public void RegisterUser_WithValidData_ReturnsTrue()
        {
            string uniqueUsername = "dsfsdfDSF";
            string password = "fndsjfnsdj@";
            string confirmPassword = "fndsjfnsdj@";

            var result = _registrationService.RegisterUser(uniqueUsername, password, confirmPassword);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(string.Empty, result.ErrorMessage);
        }

        [TestMethod]
        public void RegisterUser_WithExistingUsername_ReturnsFalse()
        {
            string username = "perfectUser";
            string password = "fndsjfnsdj@";
            string confirmPassword = "fndsjfnsdj@";

            var result = _registrationService.RegisterUser(username, password, confirmPassword);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Пользователь с таким именем уже существует.", result.ErrorMessage);
        }
    }
}