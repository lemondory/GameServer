using GameServer.Application.Features.AccountRole.Commands;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Enums;
using GameServer.Infrastructure.Result;
using GameServer.Shared.Dtos.AccountRole;
using GameServer.Shared.Enums;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Handlers
{
    public class CreateAccountRoleCommandHandler : IRequestHandler<CreateAccountRoleCommand, IRequestDataResult<CreateAccountRoleResponseDto>>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public CreateAccountRoleCommandHandler(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<IRequestDataResult<CreateAccountRoleResponseDto>> Handle(CreateAccountRoleCommand request, CancellationToken cancellationToken)
        {
            // 명령 처리 로직 추가 

            return new RequestDataResult<CreateAccountRoleResponseDto>(
                new CreateAccountRoleResponseDto()
                { 
                
                },
                true,
                ErrorCode.NONE);
        }
    }
}
