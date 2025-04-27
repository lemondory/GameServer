namespace GameServer.Shared.Dtos.AccountRole
{
    public class AccountRoleResponseDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class CreateAccountRoleResponseDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class DeleteAccountRoleResponseDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateAccountRoleResponseDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
