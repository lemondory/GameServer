using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Commands
{
    public class CreateAccountRoleCommand : IRequest<IRequestDataResult<CreateAccountRoleResponseDto>>
    {
        public CreateAccountRoleRequestDto Request { get; set; }

        public CreateAccountRoleCommand(CreateAccountRoleRequestDto request)
        {
            Request = request;   
        }
    }
}
