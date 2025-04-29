using MiniChatApp.SDK.Services.ChatRoomManager;

namespace MiniChatApp.Server.Commands
{
    public class CreateChatRoomCommand(IChatRoomManager chatRoomManager, ILogger<CreateChatRoomCommand> logger) : ChatCommand
    {
        public override CommandType Command => CommandType.CreateChatRoom;

        public override string Description => "Creates a new chat room";

        public override string Usage => "<prefix>create <room_name>";

        public override async Task ExecuteAsync()
        {
            if (Args.Length < 1)
            {
                logger.LogWarning("No room name provided.");
                return;
            }

            try
            {
                await chatRoomManager.CreateChatRoomAsync(Args[0], Message.SenderId);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to create chat room: {RoomName}, {em}", Args[0], ex.Message);
                logger.LogDebug(ex, "Failed to create chat room: {RoomName}, {ex}", Args[0], ex);
                return;
            }
            logger.LogInformation("Chat room created: {RoomName}", Args[0]);
        }
    }
}
