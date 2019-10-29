using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Logger
{
    public static class ExceptionFormater
    {

        /// <summary>
        /// Get Nested Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetNestedException(this Exception ex)
        {
            Exception current = ex;

            do
            {
                current = current.InnerException;

                if (current != null)
                    yield return current;

            } while (current != null);

        }
        /// <summary>
        /// Get Exception Detail
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="ex"></param>
        public static string GetExceptionDetail(this Exception ex,StringBuilder sb)
        {
            sb.AppendFormat("Message:{0}{1}", ex.Message, Environment.NewLine);
            sb.AppendFormat("Type:{0}{1}", ex.GetType(), Environment.NewLine);
            sb.AppendFormat("HelpLink:{0}{1}", ex.HelpLink, Environment.NewLine);
            sb.AppendFormat("Source:{0}{1}", ex.Source, Environment.NewLine);
            sb.AppendFormat("Target:{0}{1}", ex.TargetSite, Environment.NewLine);
            sb.AppendFormat("Data:{0}", Environment.NewLine);

            foreach (DictionaryEntry de in ex.Data)
                sb.AppendFormat("\t{0}:{1}{2}", de.Key, de.Value, Environment.NewLine);

            sb.AppendFormat("StackTrace:{0}{1}", ex.StackTrace, Environment.NewLine);

            return sb.ToString();
        }


    }
}
