using GameServer.Domain.Entities;
using GameServer.Shared.Models;

namespace GameServer.Contracts.Services
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AccountEntity account);
    }
}
