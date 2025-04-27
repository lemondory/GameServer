using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Contracts.GameWebSocket
{
    public interface IConnectionManager
    {
        void AddConnection(Guid accountUid, ISocket socket, int mapId);
        void RemoveConnection(Guid accountUid);
        ISocket? GetSocket(Guid accountUid);
        IEnumerable<ISocket> GetAllSockets();
        IEnumerable<Guid> GetUsersInMap(int mapId);
        void UpdateUserMap(Guid accountUid, int newMapId);
    }
}