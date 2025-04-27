using GameServer.Domain.Enums;
using GameServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Infrastructure.Result
{
    public class SuccessRequestDataResult<T> : RequestDataResult<T>
    {
        public SuccessRequestDataResult(T data, ErrorCode errorCode) : base(data, true, errorCode)
        {
        }

        public SuccessRequestDataResult(T data) : base(data, true)
        {
        }

        public SuccessRequestDataResult(ErrorCode errorCode) : base(default, true, errorCode)
        {

        }

        public SuccessRequestDataResult() : base(default, true)
        {

        }
    }
}
