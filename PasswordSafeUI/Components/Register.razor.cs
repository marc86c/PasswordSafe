using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Components.Common;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class Register
    {
        [Inject]
        public IService Service { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public UserState UserState { get; set; }

        private RegisterModel registerModel = new RegisterModel();
        private string ErrorMessage;

        protected async override Task OnInitializedAsync()
        {
            if (UserState.CurrentUser != null)
            {
                NavigationManager.NavigateTo($"/Home/{UserState.CurrentUser.Username}", true);
            }
        }

        private async Task HandleRegister()
        {
            var errorMessage = ValidatePasswordComplexity(registerModel.Password);
            if(errorMessage != string.Empty)
            {
                this.ErrorMessage = errorMessage;
                return;
            }


            var result = await Service.Register(registerModel.Username, registerModel.Password);
            if (!result)
            {
                ErrorMessage = "Registrieren ist fehlgeschlagen";
                return; 
            }
            else
            {
                ErrorMessage = string.Empty;

                NavigationManager.NavigateTo($"Home/{registerModel.Username}");
            }
        }

        public void Login()
        {
            NavigationManager.NavigateTo("/login");
        }

        public string ValidatePasswordComplexity(string passwd)
        {
            if (passwd.Length < 8 )
                return "Das Passwort muss mindestens 8 Zeichen lang sein";
            if (!passwd.Any(char.IsUpper))
                return "Das Passwort muss ein Grossbuchstaben enthalten";
            if (!passwd.Any(char.IsLower))
                return "Das Passwort muss ein Kleinbuchstaben enthalten";
            if (passwd.Contains(" "))
                return "Leerzeichen sind nicht erlaubt";
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArr = specialCh.ToCharArray();
            foreach (char ch in specialChArr)
            {
                if (passwd.Contains(ch))
                    return "Keine Spezialzeichen erlaubt";
            }

            return string.Empty;
        }
    }
}
