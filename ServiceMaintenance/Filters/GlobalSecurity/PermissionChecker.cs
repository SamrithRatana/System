using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PermissionChecker
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public PermissionChecker(IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider)
    {
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Dictionary<string, bool>> CheckPermissionsAsync(ServiceMaintenance.Contants.Modules module)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Define the permissions for each module
        var permissions = new Dictionary<string, bool>
        {
            { "Access", await AuthorizeAsync(user, module, "Access") },
            { "View", await AuthorizeAsync(user, module, "View") },
            { "Create", await AuthorizeAsync(user, module, "Create") },
            { "Edit", await AuthorizeAsync(user, module, "Edit") },
            { "Delete", await AuthorizeAsync(user, module, "Delete") }
        };

        return permissions;
    }

    private async Task<bool> AuthorizeAsync(System.Security.Claims.ClaimsPrincipal user, ServiceMaintenance.Contants.Modules module, string action)
    {
        var permission = $"Permissions.{module}.{action}";
        var result = await _authorizationService.AuthorizeAsync(user, permission);
        return result.Succeeded;
    }
}
