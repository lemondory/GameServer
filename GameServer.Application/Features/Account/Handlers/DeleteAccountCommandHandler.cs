using Dapper;
using GameServer.Application.Features.Account.Commands;
using GameServer.Domain.Enums;
using GameServer.Contracts.Infrastructure;
using MediatR;
using GameServer.Contracts.Repositories;

namespace GameServer.Application.Features.Account.Handlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return true;
        }
    }
}
