using GameServer.Application.Features.AccountRole.Commands;
using GameServer.Contracts.Repositories;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Handlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountRoleCommand, bool>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public DeleteAccountCommandHandler(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<bool> Handle(DeleteAccountRoleCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return true;
        }
    }
}
