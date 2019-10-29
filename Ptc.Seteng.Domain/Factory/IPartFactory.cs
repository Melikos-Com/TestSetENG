using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IPartFactory
    {

        /// <summary>
        /// 新增案件材料
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        Boolean Add(Tvupart part);

        /// <summary>
        /// 更新案件材料
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        Boolean Update(Tvupart part);

        /// <summary>
        /// 刪除案件材料
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        Boolean Remove(string CompCd, string Sn, string Seq);
    }
}
