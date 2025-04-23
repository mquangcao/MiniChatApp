using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Services.ChatRoomManager;
using MiniChatApp.SDK.Utils;
using MiniChatApp.Server.Commands;
using MiniChatApp.Server.Utils;

namespace MiniChatApp.Server.Factory
{
    internal class ChatCommandFactory(IServiceProvider serviceProvider) : IChatCommandFactory
    {
        public ChatCommand CreateCommand(Message message)
        {
            ArgumentNullException.ThrowIfNull(message);

            if (!CommandParser.TryParse(message.Content, out var command, out var args)) 
            {
                throw new ArgumentException("Invalid command format.");
            }
            return command switch
            {
                Protocol.CREATE_COMMAND => new CreateChatRoomCommand(serviceProvider.GetRequiredService<IChatRoomManager>(), serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<CreateChatRoomCommand>()) { Args = args, Message = message},
                _ => new SendMessageCommand(serviceProvider.GetRequiredService<IChatRoomManager>(), serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<CreateChatRoomCommand>()) { Args = args, Message = message },
            };
        }
    }
}
