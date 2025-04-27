using AutoMapper;
using GameServer.Application.Features.Auth.Command;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Infrastructure.Result;
using GameServer.Infrastructure.Security.JwtToken;
using GameServer.Shared.Dtos.Auth;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.Auth.Handlers
{
    public class TokenRefreshHandler : IRequestHandler<RefreshTokenCommand, IRequestDataResult<RefreshTokenResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public TokenRefreshHandler(IAccountRepository accountRepository, IMapper mapper, ICharacterRepository characterRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _characterRepository = characterRepository;
        }

        public async Task<IRequestDataResult<RefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByCheckRefreshTokenAsync(request.Request.AccountUID, request.Request.RefreshToken);
            if (account == null)
            {
                return new RequestDataResult<RefreshTokenResponseDto>(null, false, ErrorCode.AccountRefreshTokenNotMatch);
            }

            var lastAuth = DateTime.UtcNow;
            var tokenHelper = new JwtHelper();
            var token = tokenHelper.CreateToken(account);
            var refreshToken = tokenHelper.GenerateRefreshToken();
            account.RefreshToken = refreshToken;
            account.RefreshTokenExpiration = lastAuth.AddDays(7);

            await _accountRepository.UpdateLastLoginAndTokenAsync(account.AccountUid, refreshToken, account.RefreshTokenExpiration, lastAuth);

            // TODO json, messagepack serialization 
            return new RequestDataResult<RefreshTokenResponseDto>(new RefreshTokenResponseDto
            {
                AccessToken = token
            }, true, ErrorCode.NONE);
        }
    }
}