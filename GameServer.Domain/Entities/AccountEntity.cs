using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Domain.Entities
{
    //Entities는 핵심 비즈니스 로직의 주인공
    [Table("Account")]
    public class AccountEntity
    {
        [Key]
        [Column("account_uid")]
        public long AccountUid { get; set; }

        [Column("account_name")]
        public string AccountName { get; set; } = string.Empty;

        [Column("account_level")]
        public int AccountLevel { get; set; } = 1;

        [Column("external_provider")]
        public byte? ExternalProvider { get; set; }

        [Column("external_id")]
        public string? ExternalId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("last_login_at")]
        public DateTime LastLoginAt { get; set; }

        [Column("ban_type")]
        public byte BanType { get; set; }

        [Column("ban_end_at")]
        public DateTime BanEndAt { get; set; }

        [Column("account_role_id")]
        public int AccountRoleId { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiration")]
        public DateTime? RefreshTokenExpiration { get; set; }
    }

    public class AccountRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
