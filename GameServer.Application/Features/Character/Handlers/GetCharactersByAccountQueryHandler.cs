using AutoMapper;
using GameServer.Application.Features.Character.Queries;
using GameServer.Contracts.Repositories;
using GameServer.Shared.Dtos.Character;
using MediatR;

namespace GameServer.Application.Features.Character.Handlers
{
    public class GetCharactersByAccountQueryHandler : IRequestHandler<GetCharactersByAccountQuery, IEnumerable<CharacterSummaryDto>>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public GetCharactersByAccountQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacterSummaryDto>> Handle(GetCharactersByAccountQuery query, CancellationToken cancellationToken)
        {
            var entities = await _characterRepository.GetByAccountUIDAsync(query.AccountUID);
            return _mapper.Map<IEnumerable<CharacterSummaryDto>>(entities);
        }
    }
}
