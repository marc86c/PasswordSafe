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

        public List<AuthenticationData> AuthenticationDatas => !string.IsNullOrEmpty(filterCriteria) ? 
            user.AuthenticationDatas.Where(x => x.Username.ToLower().Contains(filterCriteria) || x.Provider.ToLower().Contains(filterCriteria)).ToList(): 
            user.AuthenticationDatas;

        public User user { get; set; }
        public bool isAdding = false;
        public AuthenticationData newData;
        public int? openPasswordIndex = null;
        public string filterCriteria;

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

        public void Logout()
        {
            UserState.CurrentUser = null;
            NavigationManager.NavigateTo("/");
        }

        public void OpenPassword(int? index)
        {
            if (index.HasValue)
            {
                openPasswordIndex = index;
            }
            else
            {
                ClosePassword();
            }
        }

        public void ClosePassword()
        {
            openPasswordIndex = null;
        }

        public async Task DeleteData(int index)
        { 
            user.AuthenticationDatas.RemoveAt(index);
            Service.UpdateUser(user);
        }

    }
}
