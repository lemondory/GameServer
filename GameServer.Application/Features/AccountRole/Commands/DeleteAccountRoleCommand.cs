using MediatR;

namespace GameServer.Application.Features.AccountRole.Commands
{
    public class DeleteAccountRoleCommand : IRequest<bool>
    {
        public DeleteAccountRoleCommand(int auid)
        {
            AUID = auid;
        }

        public int AUID { get; set; }
    }
}
