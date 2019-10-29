using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public interface IPushRepository
    {
      
        /// <summary>
        /// 依照個人推播
        /// </summary>
        /// <param name="regIds"></param>
        /// <returns></returns>
        Boolean Push(string msg, IDictionary<string, string> extras = null , params string[] regIds);


    }
}
