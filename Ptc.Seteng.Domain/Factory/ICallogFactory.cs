using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface ICallogFactory
    {

        #region 叫修流程

        /// <summary>
        /// 技師認養案件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean TechnicianAccept(Tcallog data);

        #endregion
        /// <summary>
        /// 新增案件時間歷程記錄
        /// </summary>
        /// <param name="DateRecords"></param>
        void AddDateRecords(List<TCallLogDateRecord> DateRecords);

    }
}
