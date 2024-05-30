using PasswordSafeCommon.Model;

namespace PasswordSafe.Service
{
    public interface IUserService
    {
        public User GetUserData(string username);
        public void CreateAuthenticationData (string username, AuthenticationData authenticationData);

        public void UpdateUser(User user);

    }
}
