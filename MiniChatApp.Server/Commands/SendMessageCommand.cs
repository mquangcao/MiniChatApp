using MiniChatApp.SDK.Services.ChatRoomManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.Server.Commands
{
    internal class SendMessageCommand(IChatRoomManager chatRoomManager, ILogger<CreateChatRoomCommand> logger) : ChatCommand
    {
        public override CommandType Command => CommandType.SendMessage;

        public override string Description => "Send a message to the chat room";

        public override string Usage => "<message>";

        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
