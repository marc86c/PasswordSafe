using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using PasswordSafeCommon.Model;

namespace PasswordSafe.Service
{
    public class UserService : IUserService
    {

        private readonly UserStore _userStore;
        private const string Key = "Wow-Dieser-Key-Ist-So-Krass";
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public UserService(IDataProtectionProvider dataProtectionProvider)
        {
            _userStore = new UserStore();
            _dataProtectionProvider = dataProtectionProvider;
        }

        public void CreateAuthenticationData(string username, AuthenticationData authenticationData)
        {
            authenticationData.Password = Encrypt(authenticationData.Password);

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

            var userData = _userStore.Users.First(x => x.Username == username);

            userData.AuthenticationDatas.ForEach(x => x.Password = Decrypt(x.Password));
            return _userStore.Users.First(x => x.Username == username);
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
