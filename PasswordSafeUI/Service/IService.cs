using PasswordSafeCommon.Model;

namespace PasswordSafeUI.Service
{
    public interface IService
    {
        public Task<bool> Register(string user, string password);
        public Task<User> Login(string user, string password);

        public Task<User> GetUserData(string username);
        public Task AddData(string username, AuthenticationData data);
    }
}
