using GameServer.Shared.Dtos.Character;
using MediatR;

namespace GameServer.Application.Features.Character.Queries
{
    public class GetCharactersByAccountQuery : IRequest<IEnumerable<CharacterSummaryDto>>
    {
        public long AccountUID { get; set; }

        public GetCharactersByAccountQuery(long accountUID)
        {
            AccountUID = accountUID;
        }
    }
}
