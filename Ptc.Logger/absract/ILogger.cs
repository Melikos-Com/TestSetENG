using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Logger
{
    public interface ILogger 
    {
        #region Properties

        Boolean IsDebugEnabled { get; }
        Boolean IsErrorEnabled { get; }
        Boolean IsFatalEnabled { get; }
        Boolean IsInfoEnabled  { get; }
        Boolean IsTraceEnabled { get; }
        Boolean IsWarnEnabled  { get; }

        #endregion

        #region Methods

        void Debug(Exception exception);
        void Debug(string format, params object[] args);
        void Error(Exception exception);
        void Error(string format, params object[] args);
        void Fatal(Exception exception);
        void Fatal(string format, params object[] args);
        void Info(Exception exception);
        void Info(string format, params object[] args);
        void Trace(Exception exception);
        void Trace(string format, params object[] args);
        void Warn(Exception exception);
        void Warn(string format, params object[] args);

        #endregion
    }
}
