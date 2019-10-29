using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Provider
{
    public interface ITechnicianProvider
    {
        /// <summary>
        /// 刪除技師圖片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        //Boolean DeleteImg(TvenderTechnician Data, string filePath);
        /// <summary>
        /// 新增可受理案件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Boolean AddAwaitAcceptLog(string CompCd, string Sn, string Account);
        /// <summary>
        /// 刪除可受理案件
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        Boolean RemoveAwaitAcceptLog(string CompCd,string Sn);

        int GetCallLogClaimsCount(string CompCd, string Sn);
    }

}
