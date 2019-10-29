using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Logger
{
    public class LoggerService
    {
        #region Singleton

        private static LoggerService _singletion;

        private LoggerService()
        {
        }

        public ILogger SystemLog { get; set; }

        public static LoggerService GetInstance()
        {
            if (_singletion == null)
            {
                _singletion = new LoggerService();
            }
            return _singletion;
        }
        #endregion

        public ILogger This()
        {
            return this.SystemLog;
        }
    }
}
