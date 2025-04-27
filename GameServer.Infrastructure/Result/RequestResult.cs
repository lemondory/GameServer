using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Enums;

namespace GameServer.Infrastructure.Result
{
    public class RequestResult : IRequestResult
    {
        public RequestResult(bool success, ErrorCode errorCode) : this(success)
        {
        }

        public RequestResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public ErrorCode ErrorCode { get; }
    }
}
