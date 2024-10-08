using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Chat
{
    public interface IMessageService
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync(string userId, string recipientId);
        Task DeleteMessageAsync(int messageId);
      Task<Message> GetLastMessageAsync(string userId, string recipientId); // Add this method
        Task<Message> GetMessageByIdAsync(int messageId); // Add this line


    }

}
