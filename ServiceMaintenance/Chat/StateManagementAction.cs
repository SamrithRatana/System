// NotificationService.cs
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Contants;
using ServiceMaintenance.Models;

public class StateManagementAction
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigationManager;

    public StateManagementAction(
        IHubContext<NotificationHub> hubContext,
        UserManager<ApplicationUser> userManager,
        AuthenticationStateProvider authenticationStateProvider,
        NavigationManager navigationManager)
    {
        _hubContext = hubContext;
        _userManager = userManager;
        _authenticationStateProvider = authenticationStateProvider;
        _navigationManager = navigationManager;
    }

    public async Task SendNotificationAsync(string actionType)
    {
        var article = new Article();
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        var appUser = await _userManager.GetUserAsync(user);
        var currentUserName = user.Identity?.Name ?? "Unknown User";
        var currentUserProfilePicture = appUser?.ProfilePicture;

        var currentUrl = _navigationManager.Uri;
        var moduleName = ExtractModuleNameFromUrl(currentUrl);

        article.ArticleHeading = actionType switch
        {
            "create" => $"{currentUserName} Created Successfully in {moduleName}",
            "update" => $"{currentUserName} Updated Successfully in {moduleName}",
            "delete" => $"{currentUserName} Deleted Successfully in {moduleName}",
            _ => article.ArticleHeading
        };

        var fixedArticleContent = "Service Checklist Management";

        await _hubContext.Clients.All.SendAsync("sendToUser", article.ArticleHeading, fixedArticleContent, currentUserName, currentUserProfilePicture);
    }

    private string ExtractModuleNameFromUrl(string url)
    {
        var uri = new Uri(url);
        var segments = uri.Segments;
        var moduleName = segments.Length > 1 ? segments[^1].Trim('/') : "Unknown Module";
        return moduleName;
    }
}
