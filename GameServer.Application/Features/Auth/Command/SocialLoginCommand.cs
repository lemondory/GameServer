using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.Auth;
using MediatR;

namespace GameServer.Application.Features.Auth.Command
{
    public class SocialLoginCommand : IRequest<IRequestDataResult<AuthResponseDto>>
    {
        public AuthRequestDto Request { get; set; }

        public SocialLoginCommand(AuthRequestDto request)
        {
            Request = request;
        }
    }

    public class RefreshTokenCommand : IRequest<IRequestDataResult<RefreshTokenResponseDto>>
    {
        public RefreshTokenRequestDto Request { get; set; }
        public RefreshTokenCommand(RefreshTokenRequestDto request)
        {
            Request = request;
        }
    }
}
