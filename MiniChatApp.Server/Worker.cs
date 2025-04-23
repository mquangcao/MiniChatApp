using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Services.MessageReader;
using MiniChatApp.Server.Commands;
using MiniChatApp.Server.Factory;
using MiniChatApp.Server.Models;
using System.Net;
using System.Net.Sockets;

namespace MiniChatApp.Server;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ChatServerOptions _chatServerOptions;
    private readonly IReadMessageFactory _messageReaderFactory;
    private readonly IChatCommandFactory _chatCommandFactory;

    public Worker(ILogger<Worker> logger,
        ChatServerOptions chatServerOptions,
        IReadMessageFactory messageReaderFactory,
        IChatCommandFactory chatCommandFactory)
    {
        _logger = logger;
        _chatServerOptions = chatServerOptions;
        _messageReaderFactory = messageReaderFactory;
        _chatCommandFactory = chatCommandFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var endPoint = new IPEndPoint(string.IsNullOrEmpty(_chatServerOptions.IPAddress) ? IPAddress.Any : IPAddress.Parse(_chatServerOptions.IPAddress), _chatServerOptions.Port);
        using var serverSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(endPoint);

        _logger.LogInformation("Listening... (port: {p})", _chatServerOptions.Port);
        serverSocket.Listen();

        List<ClientConnection> connections = [];
        while (!stoppingToken.IsCancellationRequested)
        {
            var clientSocket = await serverSocket.AcceptAsync(stoppingToken);
            _logger.LogInformation("Client connected: {c}", clientSocket.RemoteEndPoint);
            if (clientSocket != null)
            {
                var t = HandleClientConnectionAsync(clientSocket, stoppingToken);
                connections.Add(new ClientConnection()
                {
                    HandleTask = t,
                    Id = Guid.NewGuid()
                });
            }
        }

        Task.WaitAll(connections.Select(c => c.HandleTask).ToArray(), stoppingToken);
    }

    private async Task HandleClientConnectionAsync(Socket clientSocket, CancellationToken stoppingToken)
    {
        IMessageReader messageReader = _messageReaderFactory.Create(clientSocket);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var message = await messageReader.ReadMessageAsync(stoppingToken);
                if (message == null)
                {
                    _logger.LogWarning("Message is null");
                    break;
                }
                _logger.LogInformation("Received message: {m}", message.Content);

                var chatCommand = _chatCommandFactory.CreateCommand(message);

                _logger.LogInformation("Executing command: {c}", chatCommand.GetType().Name);
                await chatCommand.ExecuteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while handling client connection");
                break;
            }
        }
        clientSocket.Close();
    }

}
