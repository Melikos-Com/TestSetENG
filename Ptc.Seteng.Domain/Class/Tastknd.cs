using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 設備分類檔
    /// </summary>
    public class Tastknd
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 分類代號
        /// </summary>
        public string KindCd { get; set; }
        /// <summary>
        /// 分類區分
        /// </summary>
        public AssetKind TypeId { get; set; }
        /// <summary>
        /// 父節點分類代號
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public short MenuSeq { get; set; }
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string KindName { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string KindMark { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 所屬的公司資訊
        /// </summary>
        public Tcmpdat TCMPDAT { get; set; }

    }
}
