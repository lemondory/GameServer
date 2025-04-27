using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.Inventory;
using MediatR;

namespace GameServer.Application.Features.Inventory.Command
{
    public class CreateInventoryCommand : IRequest<IRequestDataResult<InventoryResponseDto>>
    {
        public long ItemUID { get; set; }
        public int TemplateID { get; set; }
        public int Count { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpireDate { get; set; } = DateTime.UtcNow;
    }
}