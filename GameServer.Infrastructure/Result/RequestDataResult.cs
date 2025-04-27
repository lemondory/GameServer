using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Enums;

namespace GameServer.Infrastructure.Result
{
    public class RequestDataResult<T> : RequestResult, IRequestDataResult<T>
    {
        public RequestDataResult(T data, bool success, ErrorCode errorCode) : base(success, errorCode)
        {
            Data = data;
            ErrorCode = errorCode;
        }

        public RequestDataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
        public ErrorCode ErrorCode { get; }
    }
}
