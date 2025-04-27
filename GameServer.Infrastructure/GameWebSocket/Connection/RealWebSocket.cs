// GameServer.Infrastructure.WebSockets/RealWebSocket.cs

using GameServer.Contracts.GameWebSocket;
using System.Net.WebSockets;
using System.Text;

namespace GameServer.Infrastructure.GameWebSocket.Connection
{
    public class RealWebSocket : ISocket
    {
        private readonly WebSocket _webSocket;

        public RealWebSocket(WebSocket webSocket)
        {
            _webSocket = webSocket ?? throw new ArgumentNullException(nameof(webSocket));
        }

        public WebSocketState State => _webSocket.State;

        public async Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            if (_webSocket.State != WebSocketState.Open)
                return;

            try
            {
                await _webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"WebSocket send failed: {ex.Message}");
            }
        }

        public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            try
            {
                return await _webSocket.ReceiveAsync(buffer, cancellationToken);
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"WebSocket receive failed: {ex.Message}");
                return new WebSocketReceiveResult(0, WebSocketMessageType.Close, true);
            }
        }

        public async Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.CloseReceived)
            {
                try
                {
                    await _webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine($"WebSocket close failed: {ex.Message}");
                }
            }
        }

        public Task SendTextAsync(string message, CancellationToken cancellationToken)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            return SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}
