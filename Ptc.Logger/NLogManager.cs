using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Logger
{
    public class NLogManager
    {
        public static LoggerService CreateInstance()
        {

            ILogger sysLogger = (ILogger)NLog.LogManager.GetLogger("System", typeof(Logger));
        
            Ptc.Logger.LoggerService.GetInstance().SystemLog = sysLogger;

            return LoggerService.GetInstance();
        }
    }
}
