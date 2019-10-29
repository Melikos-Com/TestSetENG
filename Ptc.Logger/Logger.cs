using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Logger
{
    public class Logger : NLog.Logger, Ptc.Logger.ILogger
    {
        public void Debug(Exception exception)
        {
            if (!base.IsDebugEnabled) return;

            ExceptionEvent(LogLevel.Debug, exception);
        }

        public void Debug(string format, params object[] args)
        {
            if (!base.IsDebugEnabled) return;

            var logEvent = GetLogEvent(LogLevel.Debug, null, format);

            base.Log(typeof(Logger), logEvent);

        }

        public void Error(Exception exception)
        {
            if (!base.IsErrorEnabled) return;

            ExceptionEvent(LogLevel.Error, exception);
        }

        public void Error(string format, params object[] args)
        {
            if (!base.IsErrorEnabled) return;

            var logEvent = GetLogEvent(LogLevel.Error, null, format);

            base.Log(typeof(Logger), logEvent);
        }

        public void Fatal(Exception exception)
        {
            if (!base.IsFatalEnabled) return;

            ExceptionEvent(LogLevel.Fatal, exception);

        }

        public void Fatal(string format, params object[] args)
        {
            if (!base.IsFatalEnabled) return;

            var logEvent = GetLogEvent(LogLevel.Fatal, null, format);

            base.Log(typeof(Logger), logEvent);
        }

        public void Info(Exception exception)
        {
            if (!base.IsInfoEnabled) return;

            ExceptionEvent(LogLevel.Info, exception);
        }

        public void Info(string format, params object[] args)
        {
            if (!base.IsInfoEnabled) return;


            var logEvent = GetLogEvent(LogLevel.Info, null, format);

            base.Log(typeof(Logger), logEvent);

        }

        public void Trace(Exception exception)
        {
            if (!base.IsTraceEnabled) return;

            ExceptionEvent(LogLevel.Trace, exception);

        }

        public void Trace(string format, params object[] args)
        {
            if (!base.IsTraceEnabled) return;

            var logEvent = GetLogEvent(LogLevel.Trace, null, format);

            base.Log(typeof(Logger), logEvent);
        }

        public void Warn(Exception exception)
        {
            if (!base.IsWarnEnabled) return;

            ExceptionEvent(LogLevel.Warn, exception);

        }

        public void Warn(string format, params object[] args)
        {

            if (!base.IsWarnEnabled) return;

            

            var logEvent = GetLogEvent(LogLevel.Warn, null, format);

            base.Log(typeof(Logger), logEvent);

        }

        /// <summary>
        /// Exception of Event
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        private void ExceptionEvent(LogLevel level , Exception exception)
        {
            //get main 
            var mainEvent = GetLogEvent(level,
                                        exception,
                                        Environment.NewLine + "******** main exception *******" + Environment.NewLine);

            base.Log(typeof(Logger), mainEvent);


            //get nested
            exception.GetNestedException()
                     .ToList()
                     .ForEach(exp =>
                     {
                         var subEvent = GetLogEvent(level,
                                                    exp,
                                                    Environment.NewLine + "******** inner exception *******" + Environment.NewLine);

                         base.Log(typeof(Logger), subEvent);

                     });

        }

        /// <summary>
        /// Specify Event
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private LogEventInfo GetLogEvent(LogLevel level,
                                         Exception exception,
                                         string format)
        {

            //Stack
            StackFrame frame = new StackFrame(2, false);

            StringBuilder sb = new StringBuilder();

            LogEventInfo logEvent = new LogEventInfo();

            logEvent.Level = level;

            logEvent.Message = format;

            //logEvent.Message =  ChineseConverter.Convert(format, ChineseConversionDirection.SimplifiedToTraditional);
         

            logEvent.LoggerName = $"{frame.GetMethod().DeclaringType.FullName}-{frame.GetMethod().Name}";

            if (exception != null)
            {
                logEvent.Message = format + exception.GetExceptionDetail(sb);
                logEvent.Properties["Message"] = exception.Message;
                logEvent.Properties["Type"] = exception.GetType();
                logEvent.Properties["HelpLink"] = exception.HelpLink;
                logEvent.Properties["Source"] = exception.Source;
                logEvent.Properties["Target"] = exception.TargetSite;
                logEvent.Properties["StackTrace"] = exception.StackTrace;
                logEvent.Properties["Data"] = exception.Data;
            }

            return logEvent;
        }


    }
}
