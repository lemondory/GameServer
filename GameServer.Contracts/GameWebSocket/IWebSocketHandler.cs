using System.Net.WebSockets;

namespace GameServer.Infrastructure.WebSockets.Handler
{
    public interface IWebSocketHandler
    {
        Task HandleMessageAsync(WebSocket socket, string message);
    }
}
