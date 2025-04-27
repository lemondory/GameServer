using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Security.JwtToken
{
    public class TokenOptions
    {
        public static string Audience { get; set; }
        public static string Issuer { get; set; }
        public static int AccessTokenExpiration { get; set; }
        public static int RefreshTokenExpiration { get; set; }
        public static string SecurityKey { get; set; }
    }
}
