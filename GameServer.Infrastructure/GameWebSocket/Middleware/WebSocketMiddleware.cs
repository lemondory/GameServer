// GameServer.Infrastructure.WebSockets/WebSocketMiddleware.cs

using GameServer.Contracts.GameWebSocket;
using GameServer.Infrastructure.GameWebSocket.Connection;
using GameServer.Infrastructure.GameWebSocket.Dispatcher;
using GameServer.Shared.Dtos.GameWebSocket;
using GameServer.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Text;

namespace GameServer.Infrastructure.GameWebSocket.Middleware
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionManager _connectionManager;
        private readonly WebSocketMessageDispatcher _dispatcher;

        public WebSocketMiddleware(RequestDelegate next, IConnectionManager connectionManager, WebSocketMessageDispatcher dispatcher)
        {
            _next = next;
            _connectionManager = connectionManager;
            _dispatcher = dispatcher;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            if (!Guid.TryParse(context.Request.Query["uid"], out var accountUid) ||
                !int.TryParse(context.Request.Query["map"], out var mapId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var socket = new RealWebSocket(webSocket);
            _connectionManager.AddConnection(accountUid, socket, mapId);

            await ReceiveLoop(accountUid, socket);

            _connectionManager.RemoveConnection(accountUid);
        }

        private async Task ReceiveLoop(Guid accountUid, ISocket socket)
        {
            var buffer = new byte[4096];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"[WebSocket] ({accountUid}): {message}");

                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);
                    var messageTypeStr = data?["type"]?.ToString();

                    if (!Enum.TryParse<EWebSocketMessageType>(messageTypeStr, true, out var parsedType))
                        throw new InvalidOperationException("Invalid message type enum.");

                    await _dispatcher.DispatchAsync(accountUid, parsedType.ToString(), message, socket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid JSON message: {ex.Message}");
                    var errorResponse = new WebSocketResponseDto
                    {
                        Type = "error",
                        Message = "Invalid JSON or unsupported message type"
                    };
                    var errorJson = JsonConvert.SerializeObject(errorResponse);
                    await socket.SendTextAsync(errorJson, CancellationToken.None);
                }
            }

            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
        }
    }
}
