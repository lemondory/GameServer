using GameServer.Application.Features.Inventory.Command;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Enums;
using GameServer.Infrastructure.Result;
using GameServer.Shared.Dtos.Inventory;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.Inventory.Handlers
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, IRequestDataResult<InventoryResponseDto>>
    {
        private IInventoryRepository _inventoryRepository;
        public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IRequestDataResult<InventoryResponseDto>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            // TODO 처리 위치 변경 예정

            return new RequestDataResult<InventoryResponseDto>(new InventoryResponseDto()
            {

            }, true, ErrorCode.NONE);
        }
    }
}
