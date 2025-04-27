using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Queries
{
    public class GetAccountRoleQuery : IRequest<IEnumerable<AccountRoleResponseDto>>
    {
        public long ID { get; set; }
    }
}
