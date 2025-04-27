using GameServer.Infrastructure.Errors.Middleware;
using GameServer.Infrastructure.GameWebSocket.Middleware;
using GameServer.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace GameServer.Infrastructure
{
    public static class Startup
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app
            .UseMiddleware<ExceptionMiddleware>()
            .UseMiddleware<PreRequestMiddleware>()
            .UseMiddleware<WebSocketMiddleware>();
    }
}
