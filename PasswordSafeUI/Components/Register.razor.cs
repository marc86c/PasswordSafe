﻿using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class Register
    {
        [Inject]
        public IService Service { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private RegisterModel registerModel = new RegisterModel();
        private string ErrorMessage;

        private async Task HandleRegister()
        {
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
    }
}
