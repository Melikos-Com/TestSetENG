using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IMailFactory
    {
        /// <summary>
        /// 執行寄信
        /// </summary>
        /// <returns></returns>
        Boolean Excute(MailRequest data);
    }
}
