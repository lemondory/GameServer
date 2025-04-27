using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(ApplicationServiceRegistration).Assembly);
            services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));


            //services.AddScoped<PaymentBusinessRules>();
            //services.AddScoped<AuthBusinessRules>();

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //services.AddScoped<IAuthService, AuthManager>();


            return services;

        }
    }
}
