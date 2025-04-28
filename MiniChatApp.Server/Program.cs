using MiniChatApp.Infrastructure.MongoDb;
using MiniChatApp.SDK.Repositories;
using MiniChatApp.SDK.Services.ChatRoomManager;
using MiniChatApp.Server.Factory;
using MiniChatApp.Server.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MiniChatApp.Infrastructure.InMemory;

namespace MiniChatApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddSingleton(builder.Configuration.GetRequiredSection("Server").Get<ChatServerOptions>() ?? new ChatServerOptions() { Port = 2004 });
            builder.Services.AddSingleton<IReadMessageFactory, ReadMessageFactory>();
            builder.Services.AddSingleton<IChatCommandFactory, ChatCommandFactory>();

            //builder.Services.AddSingleton(new MongoDbChatRoomOptions()
            //{
            //    ConnectionString = builder.Configuration.GetConnectionString("MiniChatApp") ?? throw new InvalidOperationException("connection string?????"),
            //    DatabaseName = builder.Configuration["ConnectionStrings:DatabaseName"]!
            //});

            builder.Services.AddSingleton<IChatRoomRepository, InMemoryChatRoomRepository>();
            builder.Services.AddSingleton<IChatRoomManager, DefaultChatRoomManager>();


            var host = builder.Build();
            host.Run();
        }
    }
}