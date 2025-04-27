using GameServer.Shared.Dtos.Account;
using MediatR;

namespace GameServer.Application.Features.Account.Commands
{
    public class UpdateAccountCommand : IRequest<AccountResponseDto>
    {
        public AccountRequestDto Request { get; set; }

        public UpdateAccountCommand(AccountRequestDto request)
        {
            Request = request;
        }
    }
}
