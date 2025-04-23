namespace MiniChatApp.Server.Models
{
    public record ChatServerOptions
    {
        public int Port { get; set; }
        public string IPAddress { get; set; } = string.Empty;
    }
}
