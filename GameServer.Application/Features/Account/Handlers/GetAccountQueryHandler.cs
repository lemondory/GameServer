using AutoMapper;
using GameServer.Application.Features.Account.Queries;
using GameServer.Contracts.Repositories;
using GameServer.Shared.Dtos.Account;
using MediatR;

namespace GameServer.Application.Features.Account.Handlers
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, IEnumerable<AccountResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResponseDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 
            // Account 정보 조회 

            return new List<AccountResponseDto>
            {
                new AccountResponseDto
                {
                    
                }
            };
        }
    }
}
