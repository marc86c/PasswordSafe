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

        protected async override Task OnInitializedAsync()
        {
            if (UserState.CurrentUser != null)
            {
                NavigationManager.NavigateTo($"/Home/{UserState.CurrentUser.Username}", true);
            }
        }

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

            if (result == null)
            {
                ErrorMessage = "Falsches Passwort";
                return;
            }

            ErrorMessage = string.Empty;
            UserState.CurrentUser = result;
            NavigationManager.NavigateTo($"Home/{loginModel.Username}");

        }

        public void Register()
        {
            NavigationManager.NavigateTo($"/register");
        }
    }
}
