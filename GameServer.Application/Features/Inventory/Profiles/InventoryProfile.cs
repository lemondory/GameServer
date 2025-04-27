using AutoMapper;
using GameServer.Application.Features.Inventory.Command;
using GameServer.Domain.Entities;
using GameServer.Shared.Dtos.Inventory;

namespace GameServer.Application.Features.Inventory.Profiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<InventoryEntity, InventoryResponseDto>().ReverseMap();
            CreateMap<CreateInventoryCommand, InventoryEntity>().ReverseMap();
            CreateMap<UpdateInventoryCommand, InventoryEntity>().ReverseMap();
        }
    }
}
