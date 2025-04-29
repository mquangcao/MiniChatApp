using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
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
            
            try
            {
                await _chatRoomCollection.InsertOneAsync(chatRoom);
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Không thể kết nối MongoDB", ex);
            }
            catch (TimeoutException ex)
            {
                throw new Exception("Truy vấn MongoDB quá lâu", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi truy vấn ChatRoom", ex);
            }
        }

        public async Task<ChatRoom?> GetChatRoomAsync(Guid chatRoomId)
        {
            try
            {
                return await _chatRoomCollection
                    .Find(cr => cr.Id == chatRoomId)
                    .FirstOrDefaultAsync();
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Không thể kết nối MongoDB", ex);
            }
            catch (TimeoutException ex)
            {
                throw new Exception("Truy vấn MongoDB quá lâu", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi truy vấn ChatRoom", ex);
            }
        }

        public Task<List<ChatRoom>> GetChatRoomsJoinAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
