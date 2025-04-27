using AutoMapper;
using GameServer.Application.Features.AccountRole.Commands;
using GameServer.Domain.Entities;
using GameServer.Shared.Dtos.AccountRole;

namespace GameServer.Application.Features.AccountRole.Profiles
{
    public class AccountRoleProfile : Profile
    {
        public AccountRoleProfile()
        {
            CreateMap<AccountEntity, AccountRoleResponseDto>().ReverseMap();
            CreateMap<CreateAccountRoleCommand, AccountRoleResponseDto>().ReverseMap();
            CreateMap<UpdateAccountRoleCommand, AccountRoleResponseDto>().ReverseMap();
        }
    }
}
