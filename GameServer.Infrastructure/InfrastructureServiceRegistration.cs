using GameServer.Contracts.GameWebSocket;
using GameServer.Contracts.Services;
using GameServer.Infrastructure.CustomExceptionFilter;
using GameServer.Infrastructure.Errors.Middleware;
using GameServer.Infrastructure.Extensions;
using GameServer.Infrastructure.GameWebSocket.Connection;
using GameServer.Infrastructure.GameWebSocket.Dispatcher;
using GameServer.Infrastructure.GameWebSocket.Handlers;
using GameServer.Infrastructure.LogEntries;
using GameServer.Infrastructure.Logging;
using GameServer.Infrastructure.Security.JwtToken;
using GameServer.Infrastructure.WebSockets.Handler;
using GameServer.Shared.Dtos.GameWebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            //services.AddSingleton<WebSocketConnectionManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();

            // Custom Middleware
            services.AddScoped<ExceptionMiddleware>();


            #region WebSocket Handlers
            services.AddSingleton<IConnectionManager, ConnectionManager>();
            services.AddSingleton<IMessageSender, MessageSender>();

            // SocketHandler 등록 
            // 방법 1. WebSocketHandlerRegistrationExtensions 사용 시 (WebSocketExtensions.cs)
            services.RegisterAllWebSocketHandlersFromAssembly();

            // 방법 2. ServiceCollectionExtensions 사용 시 (WebSocketExtensions.cs)
            //services.AddWebSocketHandler<ChatMessageHandler, ChatMessageDto>();
            //services.AddWebSocketHandler<MoveMessageHandler, MoveMessageDto>();
            
            // 방법 3. WebSocketExtensions.cs 사용하지 않을 경우 등록 방법. 아래 모두 주석 해제
            //services.AddSingleton<IWebSocketMessageHandler<ChatMessageDto>, ChatMessageHandler>();
            //services.AddSingleton<IWebSocketMessageHandler<MoveMessageDto>, MoveMessageHandler>();
            //// Dispatcher에는 object로 등록
            //services.AddSingleton<object>(sp => sp.GetRequiredService<IWebSocketMessageHandler<ChatMessageDto>>());
            //services.AddSingleton<object>(sp => sp.GetRequiredService<IWebSocketMessageHandler<MoveMessageDto>>());
            #endregion

            services.AddSingleton<WebSocketMessageDispatcher>();

            // DB 공통 처리
            //services.AddScoped<ILockService, LockService>();

            // WebSocket Services Application으로 이동? 
            //services.AddScoped<ChatService>();
            //services.AddScoped<PartyService>();

            //  Custom Filters
            services.AddTransient<LogFilter>();
            services.AddTransient<ExceptionFilter>();

            // 로그 
            services.AddSingleton<ILogService, LogService>();


            return services;

        }
    }
}
