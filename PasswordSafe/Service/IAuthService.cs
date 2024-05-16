using PasswordSafeCommon.Model;

namespace PasswordSafe.Service
{
    public interface IAuthService
    {
        public bool Register(string username, string password);
        public bool Login(string username, string password);

    }
}
