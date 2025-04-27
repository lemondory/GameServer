using GameServer.Domain.Enums;
using GameServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Result
{
    public class SuccessRequestResult : RequestResult
    {
        public SuccessRequestResult(ErrorCode errorCode) : base(true, errorCode)
        {
        }

        public SuccessRequestResult() : base(true)
        {
        }
    }
}
