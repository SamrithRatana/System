using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using ServiceMaintenance.Services;
using System.Security.Claims;

namespace ServiceMaintenance.Shared
{
    public partial class IbottomNavMenu
    {
        private bool showSettingsDrawer;
        private bool isNotificationVisible;
        private string notificationDropdownClass => isNotificationVisible ? "show" : "hide";
        [Inject]
        public IManufacturerService ManufacturerService { get; set; }
        public IEnumerable<Manufacturer> manufacturers { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private ApplicationUser user;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userClaims = authState.User;

            if (userClaims.Identity.IsAuthenticated)
            {
                var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = await UserManager.FindByIdAsync(userId);
            }
            // Fetch manufacturers from the service
            manufacturers = await ManufacturerService.GetData();
        }

        private void NavigateToHome()
        {
            NavigationManager.NavigateTo("/");
            CloseDrawer();
        }

        private void NavigateToAbout()
        {
            NavigationManager.NavigateTo("/about");
            CloseDrawer();
        }
        private void ToggleNotificationDropdown()
        {
            isNotificationVisible = !isNotificationVisible;
        }
        private void NavigateToNotification()
        {
            NavigationManager.NavigateTo("/notification");
            CloseDrawer();
        }

        private void ToggleSettingsDrawer()
        {
            showSettingsDrawer = !showSettingsDrawer;
        }

        private void CloseDrawer()
        {
            showSettingsDrawer = false;
        }

        private void dr1()
        {
            NavigationManager.NavigateTo("/some-setting");
            CloseDrawer();
        }

        private void dr2()
        {
            NavigationManager.NavigateTo("/another-setting");
            CloseDrawer();
        }

        private void dr3()
        {
            NavigationManager.NavigateTo("/another-setting");
            CloseDrawer();
        }

        private string drawerClass => showSettingsDrawer ? "show" : "hide";
    }
}
