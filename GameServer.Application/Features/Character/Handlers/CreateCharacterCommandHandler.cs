using GameServer.Application.Features.Character.Commands;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Entities;
using MediatR;

namespace GameServer.Application.Features.Character.Handlers
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterEntity>
    {
        private readonly ICharacterRepository _characterRepository;

        public CreateCharacterCommandHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<CharacterEntity> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
        {
            var req = command.Request;

            // 서버별 캐릭터 개수 제한 확인 (예: 서버당 3개)
            var count = await _characterRepository.CountByAccountAndServerAsync(req.AccountUID, req.ServerId);
            if (count >= 3)
                throw new InvalidOperationException("This account already has the maximum number of characters on this server.");

            var character = new CharacterEntity
            {
                CharacterUID = SnowflakeIdGenerator.New(), // 고유 ID 생성기 (구현 필요)
                AccountUID = req.AccountUID,
                CharacterName = req.CharacterName,
                CharacterLevel = 1,
                Exp = 0,
                ServerId = req.ServerId,
                CreatedAt = DateTime.UtcNow
            };

            await _characterRepository.CreateAsync(character);
            return character;
        }
    }

    public static class SnowflakeIdGenerator
    {
        public static long New()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() << 10 | Random.Shared.Next(0, 1024);
        }
    }
}
