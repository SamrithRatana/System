using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using ServiceMaintenance.Contants;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class PermissionConfiguration
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public PermissionConfiguration(IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider)
    {
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Dictionary<string, bool>> CheckPermissionsAsync(Modules module)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        var permissions = new Dictionary<string, bool>
            {
                { "Access", await AuthorizeAsync(user, module, "Access") },
                { "Create", await AuthorizeAsync(user, module, "Create") },
                { "Edit", await AuthorizeAsync(user, module, "Edit") },
                { "Delete", await AuthorizeAsync(user, module, "Delete") }
            };

        return permissions;
    }

    private async Task<bool> AuthorizeAsync(ClaimsPrincipal user, Modules module, string action)
    {
        var permission = $"Permissions.{module}.{action}";
        var result = await _authorizationService.AuthorizeAsync(user, permission);
        return result.Succeeded;
    }
}
