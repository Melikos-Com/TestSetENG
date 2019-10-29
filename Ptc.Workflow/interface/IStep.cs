using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Workflow
{
    public interface IStep
    {

        /// <summary>
        /// 索引
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// 執行
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Boolean Excute(ILog log);

    }
}
