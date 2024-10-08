using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ServiceMaintenance.Filters.GlobalSecurity
{
    public class PermissionBase : ComponentBase
    {
        [Inject] protected PermissionService PermissionService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private bool canAccess;
        private Dictionary<string, bool> permissions = new Dictionary<string, bool>();

        protected override async Task OnInitializedAsync()
        {
            var moduleName = GetModuleName();
            canAccess = await PermissionService.CheckModuleAccessAsync(moduleName);

            if (canAccess)
            {
                permissions = await PermissionService.CheckModulePermissionsAsync(moduleName);
            }

            if (!canAccess)
            {
                Navigation.NavigateTo("/Identity/Account/AccessDenied");
            }
        }

        protected virtual string GetModuleName() => GetType().Name;

        protected bool CanView => canAccess && permissions.GetValueOrDefault("View", false);
        protected bool CanCreate => canAccess && permissions.GetValueOrDefault("Create", false);
        protected bool CanEdit => canAccess && permissions.GetValueOrDefault("Edit", false);
        protected bool CanDelete => canAccess && permissions.GetValueOrDefault("Delete", false);
    }
}
