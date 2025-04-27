using GameServer.Shared.Dtos.Character;
using GameServer.Shared.Models;

namespace GameServer.Shared.Dtos.Auth
{
    public class AuthResponseDto
    {
        public AccessToken AccessToken { get; set; }

        public long AccountUID { get; set; }
        public string AccountName { get; set; }
        public int AccountLevel { get; set; }
        public List<CharacterSummaryDto> Characters { get; set; }
    }

    public class RefreshTokenResponseDto
    {
        public AccessToken AccessToken { get; set; }
    }
}
