using MiniChatApp.SDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.SDK.Repositories
{
    public interface IChatRoomRepository
    {
        Task CreateChatRoomAsync(ChatRoom chatRoom);
        Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId);
        Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId);
        Task<bool> AddParticipantAsync(Guid chatRoomId, Guid userId);
    }
}
