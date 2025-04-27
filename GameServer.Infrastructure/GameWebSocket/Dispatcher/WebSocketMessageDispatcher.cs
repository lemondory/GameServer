using GameServer.Contracts.GameWebSocket;
using GameServer.Shared.Dtos.GameWebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace GameServer.Infrastructure.GameWebSocket.Dispatcher
{
    public class WebSocketMessageDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _handlerTypes;

        public WebSocketMessageDispatcher(IEnumerable<object> handlers, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _handlerTypes = handlers
                .Where(h => h.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IWebSocketMessageHandler<>)))
                .GroupBy(h => (string)h.GetType().GetProperty("MessageType")!.GetValue(h)!)
                .ToDictionary(
                    g => g.Key,
                    g => g.First().GetType(),
                    StringComparer.OrdinalIgnoreCase
                );
        }

        public async Task DispatchAsync(Guid accountUid, string? messageType, string json, ISocket socket)
        {
            if (messageType == null || !_handlerTypes.TryGetValue(messageType, out var handlerType))
            {
                Console.WriteLine($"No handler found for message type: {messageType}");

                var errorResponse = new WebSocketResponseDto
                {
                    Type = "error",
                    Message = $"Unsupported message type: {messageType}"
                };
                var errorJson = JsonConvert.SerializeObject(errorResponse);
                await socket.SendTextAsync(errorJson, CancellationToken.None);

                return;
            }

            var interfaceType = handlerType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IWebSocketMessageHandler<>));

            var dtoType = interfaceType.GetGenericArguments()[0];
            var dto = JsonConvert.DeserializeObject(json, dtoType);

            var handler = _serviceProvider.GetRequiredService(interfaceType);
            var method = handlerType.GetMethod("HandleAsync");

            if (method != null)
            {
                await (Task)method.Invoke(handler, new[] { accountUid, dto!, socket })!;
            }
        }
    }
}