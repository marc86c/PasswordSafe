using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Components.Common;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class Login
    {
        [Inject]
        public IService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public UserState UserState { get; set; }

        private LoginModel loginModel = new LoginModel();
        private string ErrorMessage;

        private async Task HandleLogin()
        {
            User result;
            try
            {
                result = await Service.Login(loginModel.Username, loginModel.Password);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Falsches Passwort";
                return;
            }

            ErrorMessage = string.Empty;
            UserState.CurrentUser = result;
            NavigationManager.NavigateTo($"Home/{loginModel.Username}");

        }
    }
}
