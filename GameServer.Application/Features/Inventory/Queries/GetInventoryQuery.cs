using GameServer.Shared.Dtos.Inventory;
using MediatR;

namespace GameServer.Application.Features.Inventory.Queries
{
    public class GetInventoryQuery : IRequest<IEnumerable<InventoryResponseDto>>
    {
        public int InventoryUID { get; set; }
    }
}
