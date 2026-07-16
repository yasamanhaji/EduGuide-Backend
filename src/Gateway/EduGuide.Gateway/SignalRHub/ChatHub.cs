using Base.Application.Contracts;
using EduGuide.Application.CQRS.Messages;
using FarsiLibrary.Utils;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EduGuide.Gateway.SignalRHub
{
    [EnableCors("chat")]
    public class ChatHub(IMediator mediator, IJwtManager jwtManager, ILogger<ChatHub> logger, IMinIoService minIoService) : Hub
    {
        private static readonly ConcurrentDictionary<long, string> _connections = new();
        private static readonly ConcurrentDictionary<long, long> _activeChats = new();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var accessToken = Context.GetHttpContext().Request.Query["access_token"];
                var userId = jwtManager.GetUserId(accessToken);
                if (userId.HasValue)
                {
                    if (_connections.ContainsKey(userId.Value))
                    {
                        _connections.TryRemove(userId.Value, out _);
                        _activeChats.TryRemove(userId.Value, out _);
                        await NotifyOnlineStatus(userId.Value, false);
                    }
                }
                await base.OnDisconnectedAsync(exception);
            }
            catch { }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var accessToken = Context.GetHttpContext().Request.Query["access_token"];
                var userId = jwtManager.GetUserId(accessToken);
                if (userId.HasValue)
                {
                    if (!_connections.ContainsKey(userId.Value))
                    {
                        _connections[userId.Value] = Context.ConnectionId;
                        await NotifyOnlineStatus(userId.Value, true);
                        await SendOnlineContacts(userId.Value);
                    }
                }

                await base.OnConnectedAsync();
            }
            catch { }
        }

        public async Task SendPrivateMessage(string text, long receiverId, bool isFile = false, string filePath = null)
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            var senderId = jwtManager.GetUserId(accessToken);
            var senderName = jwtManager.GetUserName(accessToken);

            var receiver = !isFile ? await mediator.Send(new MessageCreateCommand() { SenderId = senderId.Value, ReceiverId = receiverId, Text = text })
                : await mediator.Send(new MessageCreateCommand() { SenderId = senderId.Value, ReceiverId = receiverId, Text = text, IsFile = true, FilePath = filePath});

            var sendDate = $"{PersianDateConverter.ToPersianDate(DateTime.UtcNow):yyyy/MM/dd} " + $"{DateTime.UtcNow.AddHours(3).AddMinutes(30):HH:mm}";
            if (isFile)
            {
                text = await minIoService.GetDownloadUrl(text, filePath);
            }

            // Check if the receiver is actively chatting with the sender
            bool isSeen = _activeChats.ContainsKey(receiverId) && _activeChats[receiverId] == senderId.Value;

            if (isSeen)
            {
                await mediator.Send(new SeenCommand() { MessageIds = [receiver.Value.MessageId] });
                // Send the message to the receiver if they are connected
                if (_connections.ContainsKey(receiverId))
                {
                    await Clients.Client(_connections[receiverId]).SendAsync("ReceivePrivateMessage", text, senderId, sendDate);
                }
                await Clients.Caller.SendAsync("SeenMessage", new List<long>() { receiver.Value.MessageId }, true);
                if (isFile)
                    await Clients.Caller.SendAsync("FileUrl", text);
            }
            else
            {
                // Send the message to the receiver if they are connected, even if not seen
                if (_connections.ContainsKey(receiverId))
                {
                    await Clients.Client(_connections[receiverId]).SendAsync("ReceivePrivateMessage", text, senderId, sendDate);
                }
                await Clients.Caller.SendAsync("SeenMessage", new List<long>() { receiver.Value.MessageId }, false);
                if (isFile)
                    await Clients.Caller.SendAsync("FileUrl", text);
            }
        }

        public async Task RequestOnlineContacts()
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            var userId = jwtManager.GetUserId(accessToken);

            if (userId.HasValue)
            {
                await SendOnlineContacts(userId.Value);
            }
        }

        public async Task<bool> OpenPrivateChat(long contactId) 
        {
            try
            {
                var accessToken = Context.GetHttpContext().Request.Query["access_token"];
                var userId = jwtManager.GetUserId(accessToken);

                _activeChats[userId.Value] = contactId;

                var unSeenMessagesId = await mediator.Send(new UnseenMessagesQuery() { ReceiverId = userId.Value, SenderId = contactId });

                if (_connections.ContainsKey(contactId))
                    await Clients.Client(_connections[contactId]).SendAsync("SeenMessage", unSeenMessagesId, true);

                return true;
            }
            catch { return false; }
        } 

        public bool ClosePrivateChat(long contactId)
        {
            try
            {
                var accessToken = Context.GetHttpContext().Request.Query["access_token"];
                var userId = jwtManager.GetUserId(accessToken);

                _activeChats.Remove(userId.Value, out _);
                return true;
            }
            catch { return false; }
        }

        private async Task SendOnlineContacts(long userId)
        {
            var contactIds = (await mediator.Send(new GetContactsQuery
            {
                CurrentUserId = userId
            })).Value;

            var onlineContactIds = contactIds
                .Where(id => _connections.ContainsKey(id))
                .ToList();

            await Clients.Caller.SendAsync("ReceiveOnlineContacts", onlineContactIds);
        }

        private async Task NotifyOnlineStatus(long userId, bool isOnline)
        {
            var contactIds = (await mediator.Send(new GetContactsQuery
            {
                CurrentUserId = userId
            })).Value;

            foreach (var contactId in contactIds)
            {
                if (_connections.TryGetValue(contactId, out var connectionId))
                {
                    await Clients.Client(connectionId).SendAsync(
                        "ReceiveUserStatusChange",
                        userId,
                        isOnline);
                }
            }
        }
    }
}