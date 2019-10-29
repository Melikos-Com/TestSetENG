using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public interface IMailRepository
    {
        /// <summary>
        /// 執行發信
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Send(MailRequest data);
    }
}
