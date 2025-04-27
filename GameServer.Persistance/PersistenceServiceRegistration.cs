using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Contracts.Services;
using GameServer.Persistance.Context;
using GameServer.Persistance.Repositories.Character;
using GameServer.Persistance.Repository.Account;
using GameServer.Persistance.Repository.Inventory;
using GameServer.Persistance.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GameServer.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            // insert, update, delete 공통으로 처리 
            services.AddScoped<INamedLockService, NamedLockService>();
            services.AddTransient<INamedLockExecutor, NamedLockExecutor>();

            var config = ConfigurationOptions.Parse("localhost:6379");
            config.Password = "redis";  // 여기 반드시 설정
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(config));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            // reids 
            services.AddScoped<IRedisCacheService, RedisCacheService>();
#if DEBUG
            services.AddScoped<ISerializer, JsonSerializer>();
#else
            services.AddScoped<ISerializer, MessagePackSerializer>();
#endif
            
            //string myConnStr = ApplicationSettings.DbConnString;
            //services.AddDbContext<DatabaseContext>(options => options.UseMySql(myConnStr,
            //                                                                ServerVersion.AutoDetect(myConnStr),
            //                                                                b => b.MigrationsAssembly("CqrsFramework.Api")));
            //services.AddScoped<IBrandRepository, BrandRepository>();
            //services.AddScoped<IModelRepository, ModelRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            //services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            //services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

            return services;
        }
    }
}
