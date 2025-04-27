using AutoMapper;
using GameServer.Application.Features.Inventory.Queries;
using GameServer.Application.Features.Inventory.Rules;
using GameServer.Contracts.Repositories;
using GameServer.Shared.Dtos.Inventory;
using MediatR;

namespace GameServer.Application.Features.Inventory.Handlers
{
    public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, IEnumerable<InventoryResponseDto>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public GetInventoryQueryHandler(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryResponseDto>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            GetInventoryQueryGuard.Against(request).MustBePositive();

            var inventoryEntities = await _inventoryRepository.GetInventoryAsync(request.InventoryUID);
            return _mapper.Map<IEnumerable<InventoryResponseDto>>(inventoryEntities);
        }
    }
}