using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IVendorFactory
    {
        /// <summary>
        /// 查詢技師清單
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="VendorCd"></param>
        /// <returns></returns>
        List<TvenderTechnician> GetTechnicians(string CompCd , string VendorCd);

        /// <summary>
        /// 查詢群組資訊
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        TtechnicianGroup GetGroup(string CompCd, string VendorCd , int Seq);
        /// <summary>
        /// 檢驗廠商是否啟用
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        Boolean CheckVender(string CompCd, string VendorCd);
        /// <summary>
        /// 查詢廠商負責區域
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="VendorCd"></param>
        /// <returns></returns>
        Dictionary<string, string> GetVenderZo(string CompCd, string VendorCd);
        /// <summary>
        /// 取得課別
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="ZO"></param>
        /// <returns></returns>
        List<Tzocode> SelectDo(string CompCd, String[] ZO);
    }
}
