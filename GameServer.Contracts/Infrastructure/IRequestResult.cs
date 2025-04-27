using GameServer.Shared.Enums;

namespace GameServer.Contracts.Infrastructure
{
    public interface IRequestResult
    {
        bool Success { get; }
        ErrorCode ErrorCode { get; }
    }
}
