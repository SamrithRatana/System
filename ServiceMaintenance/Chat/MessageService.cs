using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Data;
using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMaintenance.Chat
{
    public class MessageService : IMessageService
    {
        private readonly ServiceMaintenanceContext _context;

        public MessageService(ServiceMaintenanceContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(string userId, string recipientId)
        {
            return await _context.Messages
                .Where(m => (m.UserID == userId && m.RecipientID == recipientId) || (m.UserID == recipientId && m.RecipientID == userId))
                .OrderBy(m => m.When)
                .ToListAsync();
        }
        public async Task DeleteMessageAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Message> GetLastMessageAsync(string userId, string recipientId)
        {
            return await _context.Messages
                .Where(m => (m.UserID == userId && m.RecipientID == recipientId) ||
                            (m.UserID == recipientId && m.RecipientID == userId))
                .OrderByDescending(m => m.When)
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int messageId) // Add this method
        {
            return await _context.Messages.FindAsync(messageId);
        }
    }


}

