using MiniChatApp.SDK.Utils;

namespace MiniChatApp.Server.Utils
{
    internal class CommandParser
    {
        public static bool TryParse(string content, out string command, out string[] args)
        {
            ArgumentNullException.ThrowIfNull(content);

            command = string.Empty;
            args = [];

            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            var parts = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
   
            if (!parts[0].StartsWith(Protocol.PREFIX))
            {
                command = string.Empty;
                args = parts;
                return true;
            }

            command = parts[0];
            args = parts.Skip(1).ToArray();
            return true;
        }
    }
}
