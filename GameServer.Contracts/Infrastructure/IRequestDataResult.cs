using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Contracts.Infrastructure
{
    public interface IRequestDataResult<out T> : IRequestResult
    {
        T Data { get; }
    }
}
