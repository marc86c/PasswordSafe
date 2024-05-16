using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class Login
    {
        [Inject]
        public IService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private LoginModel loginModel = new LoginModel();
        private string ErrorMessage;

        private async Task HandleLogin()
        {
            var result = await Service.Login(loginModel.Username, loginModel.Password);
            if (!result)
            {
                ErrorMessage = "Invalid username or password.";
            }
            else
            {
                ErrorMessage = string.Empty;
                NavigationManager.NavigateTo($"Home/{loginModel.Username}");
                // Redirect to home or other page
            }
        }
    }
}
