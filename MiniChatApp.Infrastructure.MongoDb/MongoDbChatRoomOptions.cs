namespace MiniChatApp.Infrastructure.MongoDb
{
    public class MongoDbChatRoomOptions
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
