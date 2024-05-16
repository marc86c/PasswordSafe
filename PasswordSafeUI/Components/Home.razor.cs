using Microsoft.AspNetCore.Components;
using PasswordSafeCommon.Model;
using PasswordSafeUI.Service;

namespace PasswordSafeUI.Components
{
    public partial class Home
    {
        [Inject]
        public IService Service { get; set; }
        [Parameter]
        public string Username { get; set; }
        public User user { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = await Service.GetUserData(Username);
        }
    }
}
