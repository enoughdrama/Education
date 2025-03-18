using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AppAuthorization
{
    public class AuthService
    {
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (user == null)
                    {
                        return false;
                    }

                    bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.PasswordHash, user.Salt);
                    if (!isPasswordValid)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            return AuthenticateUserAsync(username, password).GetAwaiter().GetResult();
        }
    }
}