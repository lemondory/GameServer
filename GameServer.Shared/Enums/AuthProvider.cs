using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Enums
{
    public enum AuthProvider : short
    {
        Guest = 0,
        Google = 1,
        Apple = 2,
        Kakao = 3
    }
}
