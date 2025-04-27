namespace GameServer.Shared.Dtos.Account
{
    public class AccountResponseDto
    {
        public long AccountUID { get; set; }
        public string? AccountName { get; set; }
        public int AccountLevel { get; set; }
    }

    public class CreateAccountResponseDto
    {
        public long AccountUID { get; set; }
        public string? AccountName { get; set; }
        public int AccountLevel { get; set; }
    }

    public class DeleteAccountResponseDto
    {
        public long AccountUID { get; set; }
        public string? AccountName { get; set; }
        public int AccountLevel { get; set; }
    }

    public class UpdateAccountResponseDto
    {
        public long AccountUID { get; set; }
        public string? AccountName { get; set; }
        public int AccountLevel { get; set; }
    }
}
