using Microsoft.AspNetCore.SignalR;

namespace Project___Task_Management_Backend.Hubs
{
    public class NotificationHub : Hub
    {
        // Called by client to register itself to a user-specific group
        public async Task RegisterUser(string userId)
        {
            var groupName = GetUserGroup(userId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        // Optional: Unregister (client should call on disconnect if desired)
        public async Task UnregisterUser(string userId)
        {
            var groupName = GetUserGroup(userId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        private string GetUserGroup(string userId) => $"user-{userId}";
    }
}

