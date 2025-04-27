using GameServer.Domain.Entities;
using GameServer.Infrastructure.CustomExceptionFilter;
using GameServer.Infrastructure.LogEntries;
using GameServer.Infrastructure.Security.JwtToken;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GameServer.Application;
using GameServer.Infrastructure;
using GameServer.Persistance;
using GameServer.Infrastructure.WebSockets.Handler;
using Serilog;
using GameServer.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Serilog 설정 (파일 + Seq + 콘솔 등)
SerilogConfigurator.CreateLogger(builder.Configuration);
builder.Host.UseSerilog();

// NOTE: Appsettings degerleri uygulama baslayinca Entity Classa map edilir.
builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// JWT TOKEN kullanmak icin eklenen kodlar
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = TokenOptions.Issuer,
        ValidAudience = TokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new JwtHelper().CreateSecurityKey()
    };

#if DEBUG
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var httpContext = context.HttpContext;
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var token = httpContext.Request.Headers["Authorization"].ToString();

            LoggerHelper.LOGI("[CHALLENGE] Token is missing or invalid.");
            LoggerHelper.LOGI($"[INFO] IP Address: {ipAddress}");
            LoggerHelper.LOGI($"[INFO] User-Agent: {userAgent}");
            LoggerHelper.LOGI($"[INFO] Token: {token}");

            LoggerHelper.LOGI($"[AUTH FAILED] {context.Exception.GetType().Name}: {context.Exception.Message}");
            
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            var httpContext = context.HttpContext;
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var token = httpContext.Request.Headers["Authorization"].ToString();

            LoggerHelper.LOGI("[CHALLENGE] Token is missing or invalid.");
            LoggerHelper.LOGI($"[INFO] IP Address: {ipAddress}");
            LoggerHelper.LOGI($"[INFO] User-Agent: {userAgent}");
            LoggerHelper.LOGI($"[INFO] Token: {token}");

            LoggerHelper.LOGI("[CHALLENGE] Token is missing or invalid.");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var httpContext = context.HttpContext;
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var token = httpContext.Request.Headers["Authorization"].ToString();

            LoggerHelper.LOGI("[CHALLENGE] Token is missing or invalid.");
            LoggerHelper.LOGI($"[INFO] IP Address: {ipAddress}");
            LoggerHelper.LOGI($"[INFO] User-Agent: {userAgent}");
            LoggerHelper.LOGI($"[INFO] Token: {token}");

            LoggerHelper.LOGI("[TOKEN VALIDATED] Success!");
            return Task.CompletedTask;
        }
    };
#endif

});


builder.Host.UseSerilog((ctx, lc) =>
{
    lc.Enrich.FromLogContext()
      .Enrich.WithThreadId()
      .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 1_000_000_000, // 1GB
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 30,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(2))
      .WriteTo.Console()
      .WriteTo.Seq("http://localhost:5341");        // 로그 뷰 사이트를 자동으로 만들어 줌 TODO Config로 만들어 보자 

});

// NOTE: Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(LogFilter));
    config.Filters.Add(typeof(ExceptionFilter));
});

// WebSocket 
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromMinutes(2);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "API Swagger",
        Description = "Api Swagger Documentation",
        TermsOfService = new Uri("http://swagger.io/terms/"),
        Contact = new OpenApiContact
        {
            Name = "Mustafa Ozkan"

        }
    });
    // To Enable authorization using Swagger (JWT)  
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1a2b3c4d5e6f708090ab12cd34ef5678a9bcde1f2f3f45f67a8d9012ac345678\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// 라이브에서도 Swagger 페이지를 보려면 if문을 주석 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Websocket handler 추가 
app.UseWebSockets();
//app.Use(async (context, next) =>
//{
//    var webSocketHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();

//    if (context.Request.Path == "/ws")
//    {
//        await webSocketHandler.HandleWebSocketAsync(context);
//    }
//    else
//    {
//        await next();
//    }
//});

app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "http://localhost").AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication(); // 먼저 사용자 인증

app.UseAuthorization(); // 그 다음 권한 체크

// We register global exception handler from Infrastructer layer.
app.UseGlobalExceptionHandler();

app.MapControllers();

app.Run();
