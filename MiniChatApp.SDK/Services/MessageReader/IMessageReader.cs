using MiniChatApp.SDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.SDK.Services.MessageReader;

public interface IMessageReader
{
    Task<Message?> ReadMessageAsync(CancellationToken stoppingToken);
}
