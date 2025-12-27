using chat_app_webserver_webapi.Models;
using Microsoft.AspNetCore.SignalR;

namespace chat_app_webserver_webapi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(UserConnection userConnection)
        {
            //                       (ReveiveMessage) is keyword will used in frontend
            await Clients.All.SendAsync("ReceiveMessage", $"{userConnection.Username} has joined the chat in {userConnection.ChatRoom}.");
        }

        public async Task JoinSpecificChatRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);
            await Clients.Group(userConnection.ChatRoom).SendAsync("JoinSpecificChatRoom", $"{userConnection.Username} has joined the chat room {userConnection.ChatRoom}.");
        }

        public async Task SendMessageToChatRoom(string chatRoom, string message)
        {
            await Clients.Group(chatRoom).SendAsync("NewMessage", message);
        }
    }
}
