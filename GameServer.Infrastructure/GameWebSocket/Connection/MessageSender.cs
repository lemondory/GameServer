using GameServer.Contracts.GameWebSocket;

namespace GameServer.Infrastructure.GameWebSocket.Connection
{
    public class MessageSender : IMessageSender
    {
        private readonly IConnectionManager _connectionManager;

        public MessageSender(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task SendToUserAsync(Guid accountUid, string message, CancellationToken cancellationToken)
        {
            var socket = _connectionManager.GetSocket(accountUid);
            if (socket != null)
            {
                await socket.SendTextAsync(message, cancellationToken);
            }
        }

        public async Task SendToMapAsync(int mapId, string message, CancellationToken cancellationToken)
        {
            var users = _connectionManager.GetUsersInMap(mapId);
            foreach (var uid in users)
            {
                await SendToUserAsync(uid, message, cancellationToken);
            }
        }

        public async Task SendToAllAsync(string message, CancellationToken cancellationToken)
        {
            foreach (var socket in _connectionManager.GetAllSockets())
            {
                await socket.SendTextAsync(message, cancellationToken);
            }
        }
    }
}
