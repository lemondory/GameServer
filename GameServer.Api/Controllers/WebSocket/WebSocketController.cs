using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
//using GameServer.Infrastructure.WebSocketHandlers.Utilities;
//using GameServer.Infrastructure.WebSocketHandlers.Handler;
using System.Threading.Tasks;
using System.Threading;

namespace GameServer.Api.Controllers.WebSocket
{
    [Route("ws")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        //private readonly WebSocketConnectionManager _connectionManager;
        //private readonly IWebSocketHandler _chatHandler;
        //private readonly IWebSocketHandler _syncHandler;
        //private readonly IWebSocketHandler _partyHandler;

        //public WebSocketController(
        //    WebSocketConnectionManager connectionManager,
        //    ChatWebSocketHandler chatHandler,
        //    SyncWebSocketHandler syncHandler,
        //    PartyWebSocketHandler partyHandler)
        //{
        //    _connectionManager = connectionManager;
        //    _chatHandler = chatHandler;
        //    _syncHandler = syncHandler;
        //    _partyHandler = partyHandler;
        //}

        //[HttpGet]
        //public async Task Get()
        //{
        //    if (!HttpContext.WebSockets.IsWebSocketRequest)
        //    {
        //        HttpContext.Response.StatusCode = 400;
        //        return;
        //    }

        //    // 1. 쿼리/헤더에서 AccountUID, MapId, Type 추출
        //    var accountUidStr = HttpContext.Request.Query["accountUid"];
        //    var mapIdStr = HttpContext.Request.Query["mapId"];
        //    var type = HttpContext.Request.Query["type"];

        //    if (!long.TryParse(accountUidStr, out var accountUid) ||
        //        !int.TryParse(mapIdStr, out var mapId))
        //    {
        //        HttpContext.Response.StatusCode = 401;
        //        return;
        //    }

        //    // 2. 연결 수락
        //    var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

        //    // 3. 연결 등록
        //    _connectionManager.AddSocket(accountUid, mapId, socket);

        //    // 4. 핸들러 결정
        //    var handler = type switch
        //    {
        //        "chat" => _chatHandler,
        //        "party" => _partyHandler,
        //        "sync" => _syncHandler,
        //        _ => null
        //    };

        //    if (handler == null)
        //    {
        //        await socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid type", CancellationToken.None);
        //        _connectionManager.RemoveSocket(accountUid, mapId);
        //        return;
        //    }

        //    // 5. 핸들러에 메시지 루프 위임
        //    await handler.HandleAsync(accountUid, mapId, socket);

        //    // 6. 종료 시 연결 제거
        //    _connectionManager.RemoveSocket(accountUid, mapId);
        //}
    }
}
