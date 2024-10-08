using Microsoft.AspNetCore.SignalR;

namespace ServiceMaintenance.Pages.Parents.ItemModule
{
    public class ItemHub : Hub
    {
        public async Task BroadcastItemUpdate(string message)
        {
            // Broadcast a message to all connected clients
            await Clients.All.SendAsync("ReceiveItemUpdate", message);
        }
        public async Task BroadcastItemDelete(Guid itemId)
        {
            // Notify all connected clients about the deleted item
            await Clients.All.SendAsync("BroadcastItemDelete", itemId);
        }

    }
}
