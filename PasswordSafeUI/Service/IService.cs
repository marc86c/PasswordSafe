using PasswordSafeCommon.Model;

namespace PasswordSafeUI.Service
{
    public interface IService
    {
        public Task<bool> Register(string user, string password);
        public Task<bool> Login(string user, string password);

        public Task<User> GetUserData(string username);
    }
}
