using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Components.Common;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class UserPage
    {
        [Inject]
        public IService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public UserState UserState { get; set; }

        [Parameter]
        public string Username { get; set; }


        public User user { get; set; }
        public bool isAdding = false;
        public AuthenticationData newData;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (UserState.CurrentUser == null)
                {
                    NavigationManager.NavigateTo("/Login");
                }
                user = UserState.CurrentUser;
            }
            catch (Exception ex) 
            {
                NavigationManager.NavigateTo("/login");
            }
        }

        public void Add()
        {
            newData = new AuthenticationData();
            isAdding = true;
        }

        public async Task Save()
        {
            await Service.AddData(user.Username, newData);

            user.AuthenticationDatas.Add(newData);
            isAdding = false;
            newData = null;

            StateHasChanged();
        }
    }
}
