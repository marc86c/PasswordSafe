
using PasswordSafeCommon.Model;
using System.Net.Http;

namespace PasswordSafeUI.Service
{
    public class Service : IService
    {
        public HttpClient HttpClient;
        public Service(HttpClient httpClient) 
        { 
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri("https://localhost:7222/");
        }

        public async Task<bool> Login(string user, string password)
        {
            var loginModel = new LoginModel { Username = user, Password = password};
            var response = await HttpClient.PostAsJsonAsync("api/Auth/Login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            // Handle error response
            return false;
        }

        public async Task<bool> Register(string user, string password)
        {
            var registerModel = new RegisterModel { Username = user, Password = password };
            var response = await HttpClient.PostAsJsonAsync("api/Auth/Register", registerModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            // Handle error response
            return false;
        }

        public async Task<User> GetUserData(string username)
        {
            var response = await HttpClient.GetAsync($"api/User/User/{username}");
          
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<User>();
                return result;
            }
            throw new Exception("not found");
        }
    }
}
