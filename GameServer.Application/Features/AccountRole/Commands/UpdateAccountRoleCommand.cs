using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.AccountRole.Commands
{
    public class UpdateAccountRoleCommand : IRequest<IRequestDataResult<UpdateAccountRoleResponseDto>>
    {
        public UpdateAccountRoleResponseDto Request { get; set; }

        public UpdateAccountRoleCommand(UpdateAccountRoleResponseDto request)
        {
            Request = request;
        }
    }
}
