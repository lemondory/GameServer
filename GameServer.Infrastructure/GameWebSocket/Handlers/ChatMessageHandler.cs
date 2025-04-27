using GameServer.Contracts.GameWebSocket;
using GameServer.Shared.Dtos.GameWebSocket;

namespace GameServer.Infrastructure.GameWebSocket.Handlers
{
    public class ChatMessageHandler : IWebSocketMessageHandler<ChatMessageDto>
    {
        public string MessageType => "chat";

        public async Task HandleAsync(Guid accountUid, ChatMessageDto dto, ISocket socket)
        {
            Console.WriteLine($"[Chat] {accountUid}: {dto.Message}");
            await socket.SendTextAsync($"You said: {dto.Message}", CancellationToken.None);
        }
    }
}
