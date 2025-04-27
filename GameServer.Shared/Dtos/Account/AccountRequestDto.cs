
namespace GameServer.Shared.Dtos.Account
{
    public class AccountRequestDto 
    {
        public string AccountName { get; set; }
        public string ExternalProvider { get; set; }
        public string ExternalId { get; set; }
    }
}