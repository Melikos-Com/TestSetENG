using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 案件圖片
    /// </summary>
    public class Tcalimg
    {
        /// <summary>
        /// 公司編號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 案件序號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 圖片序號
        /// </summary>
        public byte Seq { get; set; }
        /// <summary>
        /// 圖檔型態
        /// </summary>
        public string ImgType { get; set; }
        /// <summary>
        /// 照片實體檔案
        /// </summary>
        public byte[] CallImage { get; set; }
        /// <summary>
        /// 照片類型(ex:維修前後、app上傳...等等)
        /// </summary>
        public string ImgSource { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
