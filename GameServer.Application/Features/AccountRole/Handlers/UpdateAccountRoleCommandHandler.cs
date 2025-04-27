using GameServer.Application.Features.AccountRole.Commands;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Infrastructure.Result;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Handlers
{
    public class UpdateAccountRoleCommandHandler : IRequestHandler<UpdateAccountRoleCommand, IRequestDataResult<UpdateAccountRoleResponseDto>>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public UpdateAccountRoleCommandHandler(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<IRequestDataResult<UpdateAccountRoleResponseDto>> Handle(UpdateAccountRoleCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return new RequestDataResult<UpdateAccountRoleResponseDto>(                
                new UpdateAccountRoleResponseDto()
                {

                }, true);
        }
    }
}
