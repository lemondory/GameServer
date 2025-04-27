using MediatR;

namespace GameServer.Application.Features.Account.Commands
{
    public class DeleteAccountCommand : IRequest<bool>
    {
        public DeleteAccountCommand(int auid)
        {
            AUID = auid;
        }

        public int AUID { get; set; }
    }
}
