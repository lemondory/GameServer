using AutoMapper;
using GameServer.Application.Features.Account.Commands;
using GameServer.Domain.Entities;
using GameServer.Shared.Dtos.Account;

namespace GameServer.Application.Features.Account.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountCommand, AccountEntity>().ReverseMap();
            CreateMap<AccountEntity, CreateAccountResponseDto>().ReverseMap();
            CreateMap<AccountEntity, DeleteAccountResponseDto>().ReverseMap();
            CreateMap<AccountEntity, UpdateAccountResponseDto>().ReverseMap();
        }
    }
}
