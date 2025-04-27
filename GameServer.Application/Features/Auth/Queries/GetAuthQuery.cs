//using AutoMapper;
//using GameServer.Application.Features.Account.Dtos;
//using GameServer.Application.Features.Auth.Dtos;
//using GameServer.Domain.Enums;
//using GameServer.Domain.Models;
//using GameServer.Domain.Models.Auth;
//using GameServer.Domain.Queries;
//using GameServer.Domain.Repositories;
//using GameServer.Domain.Result;
//using GameServer.Infrastructure.Result;
//using GameServer.Infrastructure.Security.JwtToken;
//using GameServer.Persistance.Context;
//using MediatR;

//namespace GameServer.Application.Features.Auth.Queries
//{
//    public class GetAuthQueryHandler : IRequestHandler<AuthRequestDto, IRequestDataResult<AuthResponseDto>>
//    {
//        private readonly IAccountRepository _accountRepository;
//        private readonly ICharacterRepository _characterRepository;
//        private readonly IMapper _mapper;
//        private readonly DatabaseContext _dbContext;

//        public GetAuthQueryHandler(IAccountRepository accountRepository, IMapper mapper, DatabaseContext dbContext, ICharacterRepository characterRepository)
//        {
//            _mapper = mapper;
//            _dbContext = dbContext;
//            _accountRepository = accountRepository;
//            _characterRepository = characterRepository;
//        }

//        public async Task<IRequestDataResult<AuthResponseDto>> Handle(AuthRequestDto request, CancellationToken cancellationToken)
//        {
//            var account = await _accountRepository.GetAccountByExternalAsync(request.AuthToken, request.Provider);
//            if (account == null)
//            {
//                return new RequestDataResult<AuthResponseDto>(null, false, "User not found");
//            }

//            var characters = await _characterRepository.GetByAccountUIDAsync(account.AccountUID);

//            var lastAuth = DateTime.UtcNow;
//            var tokenHelper = new JwtHelper();
//            var token = tokenHelper.CreateToken(account);
//            var refreshToken = tokenHelper.GenerateRefreshToken();
//            account.RefreshToken = refreshToken;
//            account.RefreshTokenExpiration = lastAuth.AddDays(7);

//            await _accountRepository.UpdateLastLoginAndTokenAsync(account.AccountUID, refreshToken, account.RefreshTokenExpiration, lastAuth);

//            // TODO json, messagepack serialization 
//            return new RequestDataResult<AuthResponseDto>(new AuthResponseDto
//            {
//                AccessToken = token,
//                AccountUID = account.AccountUID,
//                AccountName = account.AccountName,
//                AccountLevel = account.AccountLevel,
//                Characters = characters.Select(c => new CharacterSummaryDto
//                {
//                    ServerId = c.ServerId,
//                    CharacterName = c.CharacterName,
//                    CharacterLevel = c.CharacterLevel,
//                    CharacterClass = c.CharacterClass
//                }).ToList()
//            }, true, "Auth successful");
//        }
//    }
//}