using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Contracts.GameWebSocket
{
    public interface IMessageSender
    {
        Task SendToUserAsync(Guid accountUid, string message, CancellationToken cancellationToken);
        Task SendToMapAsync(int mapId, string message, CancellationToken cancellationToken);
        Task SendToAllAsync(string message, CancellationToken cancellationToken);
    }
}
