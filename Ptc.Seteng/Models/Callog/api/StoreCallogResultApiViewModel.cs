using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class StoreCallogResultApiViewModel
    {
        public StoreCallogResultApiViewModel()
        {

        }

        public StoreCallogResultApiViewModel(Tcallog data)
        {

        }
        public StoreCallogResultApiViewModel(Tcallog data, IEnumerable<TvenderTechnician> listTechnician,IEnumerable<Tusrmst> listUser)
        {

        }

        /// <summary>
        /// 立案時間
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 維修描述
        /// </summary>
        public string FixMark { get; set; }

        /// <summary>
        /// 案件編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 故障原因
        /// </summary>
        public string DamageDesc { get; set; }
        /// <summary>
        /// 叫修描述
        /// </summary>
        public string CallDesc { get; set; }
        /// <summary>
        /// 案件狀態
        /// </summary>
        public byte CallSts { get; set; }
        /// <summary>
        /// 門市地址
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// 廠商實際總成本
        /// </summary>
        public string VndTotalCost { get; set; }
        /// <summary>
        /// 完修時間
        /// </summary>
        public string FcDate { get; set; }
        /// <summary>
        /// 完修時間
        /// </summary>
        public string RcvDate { get; set; }
        /// <summary>
        /// 案件類型
        /// </summary>
        public CallKind CallKind { get; set; }
        /// <summary>
        /// 維修圖片
        /// </summary>
        public IEnumerable<string> Img { get; set; }
        /// <summary>
        /// 叫修等級
        /// </summary>
        public byte CallLevel { get; set; }
        /// <summary>
        /// 時間註記
        /// </summary>
        public string TimeDescription { get; set; }
        /// <summary>
        /// 技師名稱(認養人)
        /// </summary>
        public string AcceptedName { get; set; }
        /// <summary>
        /// 技師電話(認養人)
        /// </summary>
        public string AcceptedTel { get; set; }

        /// <summary>
        /// 總成本
        /// </summary>
        public decimal  AllCost { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        public string VenderShort { get; set; }

        /// <summary>
        /// 区域审核者名稱
        /// </summary>
        public string Zo_Manager { get; set; }

        /// <summary>
        /// TeamLeader名稱
        /// </summary>
        public string AstEng_Leader { get; set; }
    }
}