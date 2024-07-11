using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Components.Common;
using PasswordSafeUI.Service;
using System.Linq;

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

        public IQueryable<AuthenticationData> AuthenticationDatas => GetFilteredAuthenticationDatas();

        public IQueryable<AuthenticationData> GetFilteredAuthenticationDatas()
        {
            var categoriesed = filterType != null && filterType != AuthenticationDataType.None ? user.AuthenticationDatas.Where(x => x.Type == filterType) : user.AuthenticationDatas;

            return !string.IsNullOrEmpty(filterCriteria) ?
            categoriesed.Where(x => x.Username.ToLower().Contains(filterCriteria) || x.Provider.ToLower().Contains(filterCriteria)).AsQueryable() :
            categoriesed.AsQueryable();
        }

        public List<AuthenticationDataType> Types { get; set; }

        public User user { get; set; }
        public bool isAdding = false;
        public AuthenticationData newData = new AuthenticationData();
        public int? openPasswordIndex = null;
        public string filterCriteria;
        public AuthenticationDataType filterType = 0;
        public PaginationState State = new PaginationState() { ItemsPerPage = 5 };

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (UserState.CurrentUser == null)
                {
                    NavigationManager.NavigateTo("/Login");
                }
                user = UserState.CurrentUser;
                Types = Enum.GetValues(typeof(AuthenticationDataType)).Cast<AuthenticationDataType>().ToList();
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
            newData = new AuthenticationData();

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
