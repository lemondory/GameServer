using GameServer.Contracts.GameWebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebSocketHandler<THandler, TDto>(this IServiceCollection services)
            where THandler : class, IWebSocketMessageHandler<TDto>
        {
            services.AddSingleton<IWebSocketMessageHandler<TDto>, THandler>();
            services.AddSingleton<object>(sp => sp.GetRequiredService<IWebSocketMessageHandler<TDto>>());
            return services;
        }
    }

    public static class WebSocketHandlerRegistrationExtensions
    {
        public static IServiceCollection RegisterAllWebSocketHandlersFromAssembly(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IWebSocketMessageHandler<>))
                    .Select(i => new { Handler = t, Interface = i }))
                .ToList();

            foreach (var item in handlerTypes)
            {
                services.AddSingleton(item.Interface, item.Handler);
                services.AddSingleton(typeof(object), sp => sp.GetRequiredService(item.Interface));
            }

            return services;
        }
    }
}
