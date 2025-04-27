using AutoMapper;
using GameServer.Domain.Entities;
using GameServer.Shared.Dtos.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<CharacterEntity, CharacterSummaryDto>();
    }
}
