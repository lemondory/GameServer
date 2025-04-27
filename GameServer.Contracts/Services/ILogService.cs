using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Contracts.Services
{
    public interface ILogService
    {
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception? ex = null);
        void Error(string messageTemplate, Exception ex, params object[] args); // <- 추가
    }
}
