using GameServer.Domain.Enums;
using GameServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Result
{
    public class ErrorRequestResult : RequestResult
    {
        public ErrorRequestResult(ErrorCode errorCode) : base(false, errorCode)
        {
        }

        public ErrorRequestResult() : base(false)
        {
        }
    }
}
