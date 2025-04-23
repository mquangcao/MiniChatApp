using MiniChatApp.SDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.SDK.Services.ChatRoomManager
{
    public interface IChatRoomManager
    {
        Task CreateChatRoomAsync(string name, Guid managerId);
        Task<bool> AddParticipantAsync(Guid chatRoomId, Guid userId);
        Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId);
        Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId);
    }
}
