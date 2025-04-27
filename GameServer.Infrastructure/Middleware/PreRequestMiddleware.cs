using Microsoft.AspNetCore.Http;

namespace GameServer.Infrastructure.Middleware
{
    public class PreRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public PreRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 요청 처리 전에 실행할 코드
            Console.WriteLine($"[INFO] Request Path: {context.Request.Path}");

            // 다음 미들웨어로 요청 전달
            await _next(context);

            // 요청 처리 후 실행할 코드 (선택적)
        }
    }
}
