using GameServer.Application.Common.Utilities;
using GameServer.Application.Features.Auth.Command;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Contracts.Services;
using GameServer.Domain.Entities;
using GameServer.Domain.Enums;
using GameServer.Infrastructure.Result;
using GameServer.Infrastructure.Security.JwtToken;
using GameServer.Shared.Dtos.Auth;
using GameServer.Shared.Dtos.Character;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.Auth.Handlers
{
    public class SocialLoginCommandHandler : IRequestHandler<SocialLoginCommand, IRequestDataResult<AuthResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ILogService _logService;

        public SocialLoginCommandHandler(
            IAccountRepository accountRepository,
            ICharacterRepository characterRepository,
            ILogService logService)
        {
            _accountRepository = accountRepository;
            _characterRepository = characterRepository;
            _logService = logService;
        }

        public async Task<IRequestDataResult<AuthResponseDto>> Handle(SocialLoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = command.Request;
                var lastAuth = DateTime.UtcNow;

                var account = await _accountRepository.GetAccountByExternalAsync(request.AuthToken, request.Provider);

                // 계정이 없으면 생성
                if (account == null)
                {
                    var accountName = RandomStringGenerator.Generate(12);
                    var newEntity = new AccountEntity
                    {
                        AccountName = accountName,
                        ExternalProvider = (byte)request.Provider,
                        ExternalId = request.AuthToken,
                    };

                    account = await _accountRepository.CreateAccountAsync(newEntity);

                    if (account == null)
                    {
                        LoggerHelper.LOGE("계정 생성 실패");
                        return new RequestDataResult<AuthResponseDto>(null, false, ErrorCode.AccountCreateFailed);
                    }
                }

                // 토큰 생성
                var tokenHelper = new JwtHelper();
                var token = tokenHelper.CreateToken(account);

                account.RefreshToken = token.RefreshToken;
                account.RefreshTokenExpiration = token.RefreshTokenExpiration;

                await _accountRepository.UpdateLastLoginAndTokenAsync(
                    account.AccountUid,
                    token.RefreshToken,
                    token.RefreshTokenExpiration,
                    lastAuth
                );

                // 캐릭터 조회
                var characters = await _characterRepository.GetByAccountUIDAsync(account.AccountUid) ?? new List<CharacterEntity>();

                var response = new AuthResponseDto
                {
                    AccessToken = token,
                    AccountUID = account.AccountUid,
                    AccountName = account.AccountName,
                    AccountLevel = account.AccountLevel,
                    Characters = characters.Select(c => new CharacterSummaryDto
                    {
                        ServerId = c.ServerId,
                        CharacterName = c.CharacterName,
                        CharacterLevel = c.CharacterLevel,
                        CharacterClass = c.CharacterClass
                    }).ToList()
                };

                return new RequestDataResult<AuthResponseDto>(response, true, ErrorCode.NONE);
            }
            catch (Exception ex)
            {
                LoggerHelper.LOGE("소셜 로그인 처리 중 예외 발생", ex);
                return new RequestDataResult<AuthResponseDto>(null, false, ErrorCode.InternalServerError);
            }

            //var request = command.Request;
            //var lastAuth = DateTime.UtcNow;

            //var account = await _accountRepository.GetAccountByExternalAsync(request.AuthToken, request.Provider);

            //// 1. 없으면 계정 생성
            //if (account == null)
            //{
            //    var accountName = RandomStringGenerator.Generate(12);

            //    var newEntity = new AccountEntity
            //    {
            //        AccountName = accountName,
            //        ExternalProvider = (short)request.Provider,
            //        ExternalId = request.AuthToken,
            //    };

            //    account = await _accountRepository.CreateAccountAsync(newEntity);
            //    // 캐릭터는 아직 없을 수 있음
            //}

            //// 2. 토큰 생성
            //var tokenHelper = new JwtHelper();
            //var token = tokenHelper.CreateToken(account);

            //LoggerHelper.LOGI($"Token : {token.Token}");
            //LoggerHelper.LOGI($"RefreshToken : {token.RefreshToken}");

            //account.RefreshToken = token.RefreshToken;
            //account.RefreshTokenExpiration = token.RefreshTokenExpiration;

            //// 3. 로그인 시간 + 리프레시 토큰 업데이트
            //await _accountRepository.UpdateLastLoginAndTokenAsync(
            //    account.AccountUID,
            //    token.RefreshToken,
            //    token.RefreshTokenExpiration,
            //    lastAuth
            //);

            //// 4. 캐릭터 조회
            //var characters = await _characterRepository.GetByAccountUIDAsync(account.AccountUID);

            //var response = new AuthResponseDto
            //{
            //    AccessToken = token,
            //    AccountUID = account.AccountUID,
            //    AccountName = account.AccountName,
            //    AccountLevel = account.AccountLevel,
            //    Characters = characters.Select(c => new CharacterSummaryDto
            //    {
            //        ServerId = c.ServerId,
            //        CharacterName = c.CharacterName,
            //        CharacterLevel = c.CharacterLevel,
            //        CharacterClass = c.CharacterClass
            //    }).ToList()
            //};

            //return new RequestDataResult<AuthResponseDto>(response, true, ErrorCode.NONE);

            //var account = await _accountRepository.GetAccountByExternalAsync(command.Request.AuthToken, command.Request.Provider);
            //if (account == null)
            //{
            //    return new RequestDataResult<AuthResponseDto>(null, false, ErrorCode.AccountNotFound);
            //}

            //var characters = await _characterRepository.GetByAccountUIDAsync(account.AccountUID);

            //var lastAuth = DateTime.UtcNow;
            //var tokenHelper = new JwtHelper();
            //var token = tokenHelper.CreateToken(account);

            //LoggerHelper.LOGI($"Token : {token.Token}");
            //LoggerHelper.LOGI($"RefreshToken : {token.RefreshToken}");

            //await _accountRepository.UpdateLastLoginAndTokenAsync(account.AccountUID, token.RefreshToken, token.RefreshTokenExpiration, lastAuth);

            //// TODO json, messagepack serialization 
            //return new RequestDataResult<AuthResponseDto>(new AuthResponseDto
            //{
            //    AccessToken = token,
            //    AccountUID = account.AccountUID,
            //    AccountName = account.AccountName,
            //    AccountLevel = account.AccountLevel,
            //    Characters = characters.Select(c => new CharacterSummaryDto
            //    {
            //        ServerId = c.ServerId,
            //        CharacterName = c.CharacterName,
            //        CharacterLevel = c.CharacterLevel,
            //        CharacterClass = c.CharacterClass
            //    }).ToList()
            //}, true, ErrorCode.NONE);
        }
    }
}