using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameServer.Contracts.GameWebSocket
{
    public interface IWebSocketMessageHandler<T> 
    {
        string MessageType { get; }
        Task HandleAsync(Guid accountUid, T message, ISocket socket);
    }
}
