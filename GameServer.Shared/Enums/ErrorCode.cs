using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Shared.Enums
{
    public enum ErrorCode
    {
        NONE = 0,
        ACCOUNT = 100,
        AccountNotFound,                // 계정이 존재하지 않음
        AccountRefreshTokenNotMatch,    // 계정의 RefreshToken이 일치하지 않음
        AccountCreateFailed,             // 계정 생성 실패
        InternalServerError,

        INVENTORY = 200,

        ITEM = 300,

        Max,
    }    
}
