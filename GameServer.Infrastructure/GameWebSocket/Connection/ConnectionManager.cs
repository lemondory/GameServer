using GameServer.Contracts.GameWebSocket;
using System.Collections.Concurrent;

namespace GameServer.Infrastructure.GameWebSocket.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, ISocket> _connections = new();
        private readonly ConcurrentDictionary<int, ConcurrentBag<Guid>> _mapUsers = new();
        private readonly ConcurrentDictionary<Guid, int> _userMap = new();

        public void AddConnection(Guid accountUid, ISocket socket, int mapId)
        {
            _connections[accountUid] = socket;
            _userMap[accountUid] = mapId;
            _mapUsers.AddOrUpdate(mapId,
                _ => new ConcurrentBag<Guid> { accountUid },
                (_, list) => { list.Add(accountUid); return list; });
        }

        public void RemoveConnection(Guid accountUid)
        {
            _connections.TryRemove(accountUid, out _);
            if (_userMap.TryRemove(accountUid, out var mapId))
            {
                if (_mapUsers.TryGetValue(mapId, out var users))
                {
                    var updated = new ConcurrentBag<Guid>(users.Where(uid => uid != accountUid));
                    _mapUsers[mapId] = updated;
                }
            }
        }

        public ISocket? GetSocket(Guid accountUid)
        {
            return _connections.TryGetValue(accountUid, out var socket) ? socket : null;
        }

        public IEnumerable<ISocket> GetAllSockets() => _connections.Values;

        public IEnumerable<Guid> GetUsersInMap(int mapId)
        {
            return _mapUsers.TryGetValue(mapId, out var users) ? users.ToArray() : Array.Empty<Guid>();
        }

        public void UpdateUserMap(Guid accountUid, int newMapId)
        {
            if (_userMap.TryGetValue(accountUid, out var oldMapId) && oldMapId != newMapId)
            {
                RemoveFromMap(accountUid, oldMapId);
                _userMap[accountUid] = newMapId;
                _mapUsers.AddOrUpdate(newMapId,
                    _ => new ConcurrentBag<Guid> { accountUid },
                    (_, list) => { list.Add(accountUid); return list; });
            }
        }

        private void RemoveFromMap(Guid accountUid, int mapId)
        {
            if (_mapUsers.TryGetValue(mapId, out var users))
            {
                var updated = new ConcurrentBag<Guid>(users.Where(uid => uid != accountUid));
                _mapUsers[mapId] = updated;
            }
        }
    }
}
