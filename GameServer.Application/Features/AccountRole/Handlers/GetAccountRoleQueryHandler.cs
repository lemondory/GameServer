using AutoMapper;
using GameServer.Application.Features.AccountRole.Queries;
using GameServer.Contracts.Repositories;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Handlers
{
    public class GetAccountRoleQueryHandler : IRequestHandler<GetAccountRoleQuery, IEnumerable<AccountRoleResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountRoleQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountRoleResponseDto>> Handle(GetAccountRoleQuery request, CancellationToken cancellationToken)
        {
            // 계정 권한 조회

            return null;
        }
    }
}
