using Microsoft.AspNetCore.Identity;
using PasswordSafeCommon.Model;
using System.Security.Cryptography;
using System.Text;

namespace PasswordSafe.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserStore _userStore;

        public AuthService()
        {
            _userStore = new UserStore();
        }

        public bool Register(string username, string password)
        {
            if (_userStore.Users.Any(u => u.Username == username))
            {
                return false;
            }

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(password, salt);

            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            _userStore.Users.Add(user);
            _userStore.SaveChanges();

            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _userStore.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }

            var hashedPassword = HashPassword(password, user.Salt);
            return hashedPassword == user.PasswordHash;
        }

        private string GenerateSalt()
        {
            var buffer = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

