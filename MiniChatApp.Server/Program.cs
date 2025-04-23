using Microsoft.Extensions.DependencyInjection;
using MiniChatApp.Infrastructure.InMemory;
using MiniChatApp.SDK.Repositories;
using MiniChatApp.SDK.Services.ChatRoomManager;
using MiniChatApp.SDK.Services.MessageReader;
using MiniChatApp.Server.Factory;
using MiniChatApp.Server.MessageReader;
using MiniChatApp.Server.Models;
using System.Runtime.InteropServices;

namespace MiniChatApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddSingleton(builder.Configuration.GetRequiredSection("Server").Get<ChatServerOptions>() ?? new ChatServerOptions() { Port = 2004 });
            builder.Services.AddSingleton<IReadMessageFactory, ReadMessageFactory>();
            builder.Services.AddSingleton<IChatCommandFactory, ChatCommandFactory>();

            builder.Services.AddSingleton<IChatRoomRepository, InMemoryChatRoomRepository>();
            builder.Services.AddSingleton<IChatRoomManager, DefaultChatRoomManager>();


            var host = builder.Build();
            host.Run();
        }
    }
}