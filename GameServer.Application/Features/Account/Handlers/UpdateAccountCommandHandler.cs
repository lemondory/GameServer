using GameServer.Application.Features.Account.Commands;
using GameServer.Contracts.Repositories;
using GameServer.Shared.Dtos.Account;
using MediatR;

namespace GameServer.Application.Features.Account.Handlers
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountResponseDto>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponseDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return new AccountResponseDto
            {

            };
        }
    }
}
