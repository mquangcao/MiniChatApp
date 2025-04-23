using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;

namespace MiniChatApp.Infrastructure.InMemory
{
    public class InMemoryChatRoomRepository : IChatRoomRepository
    {
        private readonly Dictionary<Guid, ChatRoom> _chatRooms = [];
        public Task<bool> AddParticipantAsync(Guid chatRoomId, Guid userId)
        {
            if(_chatRooms.TryGetValue(chatRoomId, out var chatRoom))
            {
                chatRoom.Participants.Add(userId);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task CreateChatRoomAsync(ChatRoom chatRoom)
        {
            _chatRooms.Add(chatRoom.Id, chatRoom);

            return Task.CompletedTask;
        }

        public Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId)
        {
            return Task.FromResult(_chatRooms.TryGetValue(chatRoomId, out var chatRoom) ? chatRoom : null);
        }

        public Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId)
        {
            var data = _chatRooms.Where(room => room.Value.ManagerId == userId || room.Value.Participants.Contains(userId))
                .Select(room => room.Value)
                .ToList();
            return Task.FromResult(data);
        }
    }
}
