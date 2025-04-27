using GameServer.Domain.Entities;
using GameServer.Persistance.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.LogEntries
{
    public class LogFilter : IActionFilter
    {
        //private readonly DatabaseContext _context;

        public LogFilter(/*DatabaseContext context*/)
        {
            //_context = context;
        }

        LogEntity logEntity = new LogEntity();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                logEntity.id = 0;
                logEntity.username = context.HttpContext.User.Identity.IsAuthenticated == false ? "Guest" : context.HttpContext.User.Identity.Name.ToString();
                var userClaims = context.HttpContext.User.Claims.ToList();
                string userEmail = userClaims[1].Value;
                logEntity.email = context.HttpContext.User.Identity.IsAuthenticated == false ? "-" : userEmail;
                logEntity.ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                logEntity.created_at = DateTime.Now;
                logEntity.controller = context.ActionDescriptor.DisplayName;
                var myParameters = context.ActionArguments;
                string myParamsForResult = "";
                logEntity.parameters = myParameters.Aggregate(myParamsForResult, (current, p) => current + p.Key + ": " + p.Value + Environment.NewLine);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;
            var isAuthenticated = user.Identity?.IsAuthenticated ?? false;

            // 예외 발생 여부 확인
            if (context.Exception != null)
            {
                // 예외 정보 로깅
                var exception = context.Exception;
                Console.WriteLine($"[Exception] {exception.GetType().Name}: {exception.Message}");

                // DB나 외부 시스템에 예외 로그 저장 (예시)
                var logEntity = new LogEntity
                {
                    username = isAuthenticated ? user.Identity.Name : "Anonymous",
                    status = "Exception",
                    description = exception.Message,
                    type = EnumLogType.Error.ToString(),
                    created_at = DateTime.UtcNow
                };

                //_context.Logs.Add(logEntity);
                //_context.SaveChanges();

                // 예외 처리가 되었음을 명시하지 않으면 이후에 다시 throw될 수 있음
                context.ExceptionHandled = true;
            }
            else if (isAuthenticated)
            {
                // 정상 응답 로깅
                var statusCode = httpContext.Response.StatusCode;
                var logEntity = new LogEntity
                {
                    username = user.Identity.Name,
                    status = statusCode.ToString(),
                    description = "-",
                    type = statusCode == 200 ? EnumLogType.Log.ToString() : EnumLogType.Warning.ToString(),
                    created_at = DateTime.UtcNow
                };

                //_context.Logs.Add(logEntity);
                //_context.SaveChanges();
            }
        }
    }
}
