using GameServer.Application.Features.Inventory.Command;
using GameServer.Contracts.Repositories;
using MediatR;

namespace GameServer.Application.Features.Inventory.Handlers
{
    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, bool>
    {
        private IInventoryRepository _inventoryRepository;

        public DeleteInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<bool> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {

            return false;
        }
    }
}
