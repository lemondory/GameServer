using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Dtos.AccountRole
{
    public class AccountRoleRequestDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class CreateAccountRoleRequestDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class DeletaAccountRoleRequestDto
    {
        public int Id { get; set; }
    }

    public class UpdateAccountRoleRequestDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
