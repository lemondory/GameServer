namespace GameServer.Shared.Dtos.Auth
{
    public class AuthRequestDto
    {
        public int Provider { get; set; }  // enum
        public string? AuthToken { get; set; }      // Guest는 null 가능
    }

    public class RefreshTokenRequestDto
    {
        public int AccountUID { get; set; }
        public string RefreshToken { get; set; }
    }
}