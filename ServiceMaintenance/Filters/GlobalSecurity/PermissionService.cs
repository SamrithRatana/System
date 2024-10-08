using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace ServiceMaintenance.Filters.GlobalSecurity
{
    public class PermissionService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public PermissionService(IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider)
        {
            _authorizationService = authorizationService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> CheckModuleAccessAsync(string moduleName)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // Check module-level access
            var accessPermission = $"Permissions.{moduleName}.Access";
            return (await _authorizationService.AuthorizeAsync(user, accessPermission)).Succeeded;
        }

        public async Task<Dictionary<string, bool>> CheckModulePermissionsAsync(string moduleName)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var permissions = new Dictionary<string, bool>
            {
                { "View", (await _authorizationService.AuthorizeAsync(user, $"Permissions.{moduleName}.View")).Succeeded },
                { "Create", (await _authorizationService.AuthorizeAsync(user, $"Permissions.{moduleName}.Create")).Succeeded },
                { "Edit", (await _authorizationService.AuthorizeAsync(user, $"Permissions.{moduleName}.Edit")).Succeeded },
                { "Delete", (await _authorizationService.AuthorizeAsync(user, $"Permissions.{moduleName}.Delete")).Succeeded }
            };

            return permissions;
        }
    }
}
