using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ServiceMaintenance.Chat;
 using ServiceMaintenance.Models;
using System.Collections.Concurrent;

    public class ChatHub : Hub
    {
    private static readonly ConcurrentDictionary<string, bool> OnlineUsers = new ConcurrentDictionary<string, bool>();

    private readonly IMessageService _messageService;
     private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;

        }
    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;

        if (!string.IsNullOrEmpty(userId))
        {
            OnlineUsers[userId] = true; // Mark the user as online
            Clients.All.SendAsync("UserConnected", userId);
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;

        if (!string.IsNullOrEmpty(userId))
        {
            OnlineUsers.TryRemove(userId, out _); // Remove the user safely
            Clients.All.SendAsync("UserDisconnected", userId);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public Task<IEnumerable<string>> GetOnlineUsers()
    {
        // Return the list of online users
        return Task.FromResult(OnlineUsers.Keys.AsEnumerable());
    }
    public async Task SendMessage(string userName, string messageText, string userId, string recipientId, string timestamp, string fileUrl = null, string audioUrl = null)
        {
        // Assuming you have a way to retrieve the user's profile picture, e.g., using UserManager
        var senderUser = await _userManager.FindByIdAsync(userId); // Assuming _userManager is injected
        var profilePictureBase64 = Convert.ToBase64String(senderUser.ProfilePicture); // Assuming ProfilePicture is a byte array

        var message = new Message
            {
                UserName = userName,
                Text = messageText,
                When = DateTime.Parse(timestamp),
                UserID = userId,
                RecipientID = recipientId,
                FileUrl = fileUrl,
                AudioURL = audioUrl // Include the audio URL
            };

            await _messageService.SaveMessageAsync(message);

            // Send to all clients (or specify recipient if needed)
            await Clients.All.SendAsync("ReceiveMessage", userName, messageText, timestamp, fileUrl, audioUrl, message.Id);
        // Update last message for both the sender and the recipient in real-time
        await Clients.All.SendAsync("UpdateLastMessage", userId, recipientId, messageText, timestamp);

        await Clients.User(recipientId).SendAsync("ReceiveNotification", $"sent you a message.", profilePictureBase64, userName);
    }

    public async Task<IEnumerable<Message>> GetChatHistory(string userId, string recipientId)
        {
            return await _messageService.GetMessagesAsync(userId, recipientId);
        }
    public async Task DeleteMessage(int messageId)
    {
        // Retrieve the message to be deleted
        var message = await _messageService.GetMessageByIdAsync(messageId);
        if (message == null) return;

        // Delete the message
        await _messageService.DeleteMessageAsync(messageId);

        // Notify clients to remove the deleted message
        await Clients.All.SendAsync("MessageDeleted", messageId);

        // Update last messages for sender and recipient
        var sender = await _userManager.FindByIdAsync(message.UserID);
        var recipient = await _userManager.FindByIdAsync(message.RecipientID);

        if (sender != null && recipient != null)
        {
            var lastSenderMessage = await _messageService.GetLastMessageAsync(sender.Id, recipient.Id);
            var lastRecipientMessage = await _messageService.GetLastMessageAsync(recipient.Id, sender.Id);

            await Clients.All.SendAsync("UpdateLastMessage", sender.Id, recipient.Id,
                lastSenderMessage?.Text ?? "No messages",
                lastSenderMessage?.When.ToString("o") ?? DateTime.Now.ToString("o"));

            await Clients.All.SendAsync("UpdateLastMessage", recipient.Id, sender.Id,
                lastRecipientMessage?.Text ?? "No messages",
                lastRecipientMessage?.When.ToString("o") ?? DateTime.Now.ToString("o"));
        }
    }


}





