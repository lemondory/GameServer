using AutoMapper;

namespace GameServer.Application.Features.Auth.Profiles
{
    // 서로 다른 객체들 간의 자동 변환 규칙을 설정.
    // 주로 DTO <> Entity 간 변환에 사용되며, 코드를 간결하게 만들어줍니다.
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            //CreateMap<GetLoginQuery, LoginResponse>().ReverseMap();
            //CreateMap<CreateAuthCommand, AuthEntity>().ReverseMap();
            //CreateMap<UpdateAuthCommand, AuthEntity>().ReverseMap();
        }
    }
}
