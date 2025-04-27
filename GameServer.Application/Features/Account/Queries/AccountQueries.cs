using GameServer.Shared.Dtos.Account;
using MediatR;

namespace GameServer.Application.Features.Account.Queries
{
    public class GetAccountQuery : IRequest<IEnumerable<AccountResponseDto>>
    {
        public long AUID { get; set; }
    }
}
