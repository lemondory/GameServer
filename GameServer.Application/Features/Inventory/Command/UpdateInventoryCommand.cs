using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.Inventory;
using MediatR;

namespace GameServer.Application.Features.Inventory.Command
{
    public class UpdateInventoryCommand : IRequest<IRequestDataResult<UpdateInventoryResponseDto>>
    {
        public UpdateInventoryRequestDto Request { get; set; }

        public UpdateInventoryCommand(UpdateInventoryRequestDto request)
        {
            Request = request;
        }
    }
}