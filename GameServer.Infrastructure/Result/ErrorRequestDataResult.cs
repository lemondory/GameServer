using GameServer.Domain.Enums;
using GameServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Result
{
    public class ErrorRequestDataResult<T> : RequestDataResult<T>
    {
        public ErrorRequestDataResult(T data, ErrorCode errorCode) : base(data, false, errorCode)
        {
        }

        public ErrorRequestDataResult(T data) : base(data, false)
        {
        }

        public ErrorRequestDataResult(ErrorCode errorCode) : base(default, false, errorCode)
        {

        }

        public ErrorRequestDataResult() : base(default, false)
        {

        }
    }
}
