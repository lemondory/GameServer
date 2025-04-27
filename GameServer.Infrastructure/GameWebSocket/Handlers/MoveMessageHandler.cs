using GameServer.Contracts.GameWebSocket;
using GameServer.Shared.Dtos.GameWebSocket;

namespace GameServer.Infrastructure.GameWebSocket.Handlers
{
    public class MoveMessageHandler : IWebSocketMessageHandler<MoveMessageDto>
    {
        public string MessageType => "move";

        public async Task HandleAsync(Guid accountUid, MoveMessageDto message, ISocket socket)
        {
            if (message != null)
            {
                Console.WriteLine($"[Move] {accountUid} moved to ({message.X}, {message.Y})");
                await socket.SendTextAsync($"Moved to ({message.X}, {message.Y})", CancellationToken.None);
            }
        }
    }
}
