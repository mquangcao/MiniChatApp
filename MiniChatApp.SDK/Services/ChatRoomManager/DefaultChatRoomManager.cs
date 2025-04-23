using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;

namespace MiniChatApp.SDK.Services.ChatRoomManager;

public class DefaultChatRoomManager(IChatRoomRepository _chatRoomRepository) : IChatRoomManager
{

    public Task<bool> AddParticipantAsync(Guid chatRoomId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateChatRoomAsync(string name, Guid managerId)
    {
        ChatRoom chatRoom = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            ManagerId = managerId,
            Participants = [managerId]
        };

        await _chatRoomRepository.CreateChatRoomAsync(chatRoom);
    }

    public Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId)
    {
        return _chatRoomRepository.GetChatRoomsJoinAsync(userId);
    }
}
