using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AppAuthorization
{
    public class RegistrationService
    {
        public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(string username, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                return (false, "Заполните все поля.");
            }

            if (password != confirmPassword)
            {
                return (false, "Пароли не совпадают.");
            }

            if (!IsValidPassword(password, out string passwordError))
            {
                return (false, passwordError);
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (existingUser != null)
                    {
                        return (false, "Пользователь с таким именем уже существует.");
                    }

                    PasswordHelper.CreatePasswordHash(password, out string passwordHash, out string salt);

                    var newUser = new User
                    {
                        Username = username,
                        PasswordHash = passwordHash,
                        Salt = salt
                    };

                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при регистрации: {ex.Message}");
            }
        }

        public (bool Success, string ErrorMessage) RegisterUser(string username, string password, string confirmPassword)
        {
            return RegisterUserAsync(username, password, confirmPassword).GetAwaiter().GetResult();
        }

        private bool IsValidPassword(string password, out string error)
        {
            error = string.Empty;

            if (password.Length < 8)
            {
                error = "Пароль должен содержать не менее 8 символов.";
                return false;
            }

            string allowedPattern = @"^[A-Za-z!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$";
            if (!Regex.IsMatch(password, allowedPattern))
            {
                error = "Пароль может содержать только английские буквы и спецсимволы.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[A-Za-z]"))
            {
                error = "Пароль должен содержать хотя бы одну английскую букву.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            {
                error = "Пароль должен содержать хотя бы один спецсимвол.";
                return false;
            }

            return true;
        }
    }
}