using GameServer.Application.Features.Inventory.Command;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Infrastructure.Result;
using GameServer.Shared.Dtos.Inventory;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.Inventory.Handlers
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, IRequestDataResult<UpdateInventoryResponseDto>>
    {
        private IInventoryRepository _inventoryRepository;

        public UpdateInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IRequestDataResult<UpdateInventoryResponseDto>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            // Service로 옮겨 처리 

            return new RequestDataResult<UpdateInventoryResponseDto>(new UpdateInventoryResponseDto(), true, ErrorCode.NONE);
        }
    }
}
