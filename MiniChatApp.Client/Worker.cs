using System.Net;
using System.Net.Sockets;

namespace MiniChatApp.Client
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, 3010);
            var clientSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );
            await clientSocket.ConnectAsync(endPoint, stoppingToken);
            _logger.LogInformation("Connected to server at {IPAddress}:{Port}", endPoint.Address, endPoint.Port);





            string msg = @"senderId: A8604AEC-6A3B-404E-8716-21BB6398C2D4
         receiverId: A8604AEC-6A3B-404E-8716-21BB6398C2D4

         /create hehe
";
            var stream = new NetworkStream(clientSocket);
            var writer = new StreamWriter(stream);

            ReadOnlyMemory<char> mem = msg.AsMemory();
            await writer.WriteAsync(mem, stoppingToken);
            await writer.FlushAsync(stoppingToken);
            await Task.CompletedTask;
        }
    }
}
