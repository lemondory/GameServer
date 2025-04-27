using MediatR;
using GameServer.Domain.Entities;
using GameServer.Shared.Dtos.Character;

namespace GameServer.Application.Features.Character.Commands
{
    public class CreateCharacterCommand : IRequest<CharacterEntity>
    {
        public CreateCharacterRequestDto Request { get; set; }

        public CreateCharacterCommand(CreateCharacterRequestDto request)
        {
            Request = request;
        }
    }
}