using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.SignalR;
using ServiceMaintenance.Data;
using ServiceMaintenance.Models;
using System.Threading.Tasks;

namespace ServiceMaintenance.Contants
{
    public class NotificationHub : Hub
    {
        
        public async Task SendNotification(string heading, string content, string username, byte[] profilePicture)
        {
            var base64ProfilePicture = profilePicture != null ? Convert.ToBase64String(profilePicture) : null;          
            await Clients.All.SendAsync("sendToUser", heading, content, username, base64ProfilePicture);
        }
    }
}
