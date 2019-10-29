using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public interface IImgRepository
    {
        /// <summary>
        /// 取得門市店名
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Store"></param>
        /// <returns></returns>
        string GetStoreName(string CompCd, string StoreCd);
        /// <summary>
        /// 取得案件的待受理技師
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        Dictionary<string,string> GetAwaitAdoptTechnician(Tcallog data);
        /// <summary>
        /// 取得案件的受理技師
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        Dictionary<string, string> GetAcceptTechnician(string CompCd, string Sn);
        /// <summary>
        /// 取得設施的設備分類
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Asset"></param>
        /// <returns></returns>
        string GetSpcAssetKind(string CompCd, string Asset);
        /// <summary>
        /// 查詢照片
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        IEnumerable<string> SearchImg(string CompCd, string Sn, int img);

        /// <summary>
        /// 新增照片
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        void AddImg(Tcallog input);

        /// <summary>
        /// 取得下載連結
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string GetLink(string index);
    }
}
