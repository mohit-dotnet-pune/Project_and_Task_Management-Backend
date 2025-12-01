using Microsoft.AspNetCore.SignalR;
using Project___Task_Management_Backend.Hubs;
using Project___Task_Management_Backend.Models;
namespace Project___Task_Management_Backend.Services
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        // Send to a single user (via group user-{userId})
        public Task SendToUserAsync(string userId, NotificationMessage message)
        {
            var group = GetGroup(userId);
            return _hub.Clients.Group(group).SendAsync("ReceiveNotification", message);
        }

        // Broadcast to all connected clients
        public Task BroadcastAsync(NotificationMessage message)
        {
            return _hub.Clients.All.SendAsync("ReceiveNotification", message);
        }

        // Send to multiple users
        public Task SendToUsersAsync(IEnumerable<string> userIds, NotificationMessage message)
        {
            var tasks = userIds.Select(u => SendToUserAsync(u, message));
            return Task.WhenAll(tasks);
        }

        private static string GetGroup(string userId) => $"user-{userId}";
    }
}
