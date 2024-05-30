using Microsoft.AspNetCore.Identity;
using PasswordSafeCommon.Model;

namespace PasswordSafe.Service
{
    public class UserService : IUserService
    {

        private readonly UserStore _userStore;

        public UserService()
        {
            _userStore = new UserStore();
        }

        public void CreateAuthenticationData(string username, AuthenticationData authenticationData)
        {
            _userStore.Users.First(x => x.Username == username).AuthenticationDatas.Add(authenticationData);
            _userStore.SaveChanges();
        }


        public void UpdateUser(User user)
        {
            var currentUser = _userStore.Users.First(x => x.Username == user.Username);
            _userStore.Users.RemoveAt(_userStore.Users.IndexOf(currentUser));
            _userStore.Users.Add(user);
            _userStore.SaveChanges();
        }

        public User GetUserData(string username)
        {
            return _userStore.Users.First(x => x.Username == username);
        }
    }
}
