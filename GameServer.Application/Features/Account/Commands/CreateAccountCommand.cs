using GameServer.Contracts.Infrastructure;
using GameServer.Shared.Dtos.Account;
using GameServer.Shared.Dtos.AccountRole;
using MediatR;

namespace GameServer.Application.Features.Account.Commands
{
    public class CreateAccountCommand : IRequest<IRequestDataResult<CreateAccountResponseDto>>
    {
        public AccountRequestDto Request { get; set; }

        public CreateAccountCommand(AccountRequestDto request)
        {
            Request = request;
        }
    }
}
