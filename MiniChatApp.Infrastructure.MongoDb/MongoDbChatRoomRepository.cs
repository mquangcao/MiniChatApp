using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MiniChatApp.Infrastructure.MongoDb
{
    public class MongoDbChatRoomRepository : IChatRoomRepository
    {
        private readonly IMongoCollection<ChatRoom> _chatRoomCollection;
        public MongoDbChatRoomRepository(MongoDbChatRoomOptions options)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            var client = new MongoClient(options.ConnectionString);
            
            var database = client.GetDatabase(options.DatabaseName);
            _chatRoomCollection = database.GetCollection<ChatRoom>("ChatRooms");

        }
        public Task<bool> AddParticipantAsync(Guid chatRoomId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateChatRoomAsync(ChatRoom chatRoom)
        {
            await _chatRoomCollection.InsertOneAsync(chatRoom);
        }

        public async Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId)
        {
            return await _chatRoomCollection
                .Find(cr => cr.Id == chatRoomId)
                .FirstOrDefaultAsync();
        }

        public Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
