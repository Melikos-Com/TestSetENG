using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CallogDetailViewModel
    {


        public CallogDetailViewModel()
        {

        }

        public CallogDetailViewModel(Tcallog data , FeatureType type)
        {

        }

        #region 案件明細

        /// <summary>
        /// 叫修編號
        /// </summary>
        [DisplayName("叫修編號")]
        [DisplayFormat(NullDisplayText = "叫修編號")]
        public string Sn { get; set; }


        /// <summary>
        /// 公司代號
        /// </summary>
        [DisplayName("公司代號")]
        [DisplayFormat(NullDisplayText = "公司代號")]
        public string CompCd { get; set; }


        /// <summary>
        /// 緊急程度
        /// </summary>
        [DisplayName("緊急程度")]
        [DisplayFormat(NullDisplayText = "緊急程度")]
        public string CallLevel { get; set; }

        /// <summary>
        /// 案件類型
        /// </summary>
        [DisplayName("案件類型")]
        [DisplayFormat(NullDisplayText = "案件類型")]
        public string CallKind { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        [DisplayFormat(NullDisplayText = "公司名稱")]
        public string CompName { get; set; }

        /// <summary>
        /// 區域名稱
        /// </summary>
        [DisplayName("區域名稱")]
        [DisplayFormat(NullDisplayText = "區域名稱")]
        public string ZoName { get; set; }

        /// <summary>
        /// 門市名稱
        /// </summary>
        [DisplayName("門市名稱")]
        [DisplayFormat(NullDisplayText = "門市名稱")]
        public string StoreName { get; set; }

        /// <summary>
        /// 門市編號
        /// </summary>
        [DisplayName("門市編號")]
        [DisplayFormat(NullDisplayText = "門市編號")]
        public string StoreCd { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        [DisplayName("設備名稱")]
        [DisplayFormat(NullDisplayText = "設備名稱")]
        public string AssetName { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        [DisplayName("設備編號")]
        [DisplayFormat(NullDisplayText = "設備編號")]
        public string AssetCd { get; set; }


        /// <summary>
        /// 故障原因
        /// </summary>
        [DisplayName("故障原因")]
        [DisplayFormat(NullDisplayText = "故障原因")]
        public string DamageDesc { get; set; }

        /// <summary>
        /// 保固狀態
        /// </summary>
        [DisplayName("保固狀態")]
        [DisplayFormat(NullDisplayText = "保固狀態")]
        public string AstType { get; set; }

        /// <summary>
        /// 設施系統編號
        /// </summary>
        [DisplayName("設施系統編號")]
        [DisplayFormat(NullDisplayText = "設施系統編號")]
        public string AssetSeq { get; set; }

        /// <summary>
        /// 設施財產編號
        /// </summary>
        [DisplayName("設施財產編號")]
        [DisplayFormat(NullDisplayText = "設施財產編號")]
        public string AssetNo { get; set; }

        /// <summary>
        /// 誤叫修狀態
        /// </summary>
        [DisplayName("誤叫修狀態")]
        [DisplayFormat(NullDisplayText = "誤叫修狀態")]
        public string CancelCaseType { get; set; }

        /// <summary>
        /// 報修人員
        /// </summary>
        [DisplayName("報修人員")]
        [DisplayFormat(NullDisplayText = "報修人員")]
        public string CreateUser { get; set; }

        /// <summary>
        /// 報修時間
        /// </summary>
        [DisplayName("報修時間")]
        [DisplayFormat(NullDisplayText = "報修時間")]
        public string CreateDate { get; set; }

        /// <summary>
        /// 是否重複叫修
        /// </summary>
        [DisplayName("是否重複叫修")]
        [DisplayFormat(NullDisplayText = "是否重複叫修")]
        public Boolean IsRepeat { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        [DisplayName("故障描述")]
        [DisplayFormat(NullDisplayText = "故障描述")]
        public string CallDesc { get; set; }

        /// <summary>
        /// CC備註
        /// </summary>
        [DisplayName("CC備註")]
        [DisplayFormat(NullDisplayText = "CC備註")]
        public string CCRemark { get; set; }

        #endregion

        #region 銷案資訊

        /// <summary>
        /// 廠商名稱
        /// </summary>
        [DisplayName("廠商名稱")]
        [DisplayFormat(NullDisplayText = "廠商名稱")]
        public string VenderName { get; set; }

        /// <summary>
        /// 廠商卡號
        /// </summary>
        [DisplayName("廠商卡號")]
        [DisplayFormat(NullDisplayText = "廠商卡號")]
        public string VndCardNo { get; set; }

        /// <summary>
        /// 應完成時間
        /// </summary>
        [DisplayName("應完成時間")]
        [DisplayFormat(NullDisplayText = "應完成時間")]
        public string FdDate { get; set; }

        /// <summary>
        /// 實際完成時間
        /// </summary>
        [DisplayName("實際完成時間")]
        [DisplayFormat(NullDisplayText = "實際完成時間")]
        public string FcDate { get; set; }

        /// <summary>
        /// 是否超時
        /// </summary>
        [DisplayName("是否超時")]
        [DisplayFormat(NullDisplayText = "是否超時")]
        public string IsOvertime { get; set; }

        /// <summary>
        /// 門市備註
        /// </summary>
        [DisplayName("門市備註")]
        [DisplayFormat(NullDisplayText = "門市備註")]
        public string StoreCfmRemark { get; set; }

        /// <summary>
        /// 維修人員
        /// </summary>
        [DisplayName("維修人員")]
        [DisplayFormat(NullDisplayText = "維修人員")]
        public string EngName { get; set; }

        /// <summary>
        /// 審核備註
        /// </summary>
        [DisplayName("審核備註")]
        [DisplayFormat(NullDisplayText = "審核備註")]
        public string Remark { get; set; }

        /// <summary>
        /// 逾時時數
        /// </summary>
        [DisplayName("逾時時數")]
        [DisplayFormat(NullDisplayText = "逾時時數")]
        public int OvertimeHour { get; set; }

        /// <summary>
        /// 主管審核備註
        /// </summary>
        [DisplayName("主管審核備註")]
        [DisplayFormat(NullDisplayText = "主管審核備註")]
        public string BossCfmRemark { get; set; }
        #endregion

        #region 材料資訊

        /// <summary>
        /// 材料序號
        /// </summary>
        public string Seq { get; set; }

        /// <summary>
        /// 材料資訊
        /// </summary>
        public String[][] MaterialList { get; set; }

        #endregion

        #region Extend

        /// <summary>
        /// 功能進入點
        /// </summary>
        public FeatureType FeatureType { get; set; }

        /// <summary>
        /// 操作模式
        /// </summary>
        public AuthNodeType AuthNodeType { get; set; }

        #endregion

        #region method

        public decimal ComputeOvertimeFine(DateTime start  , DateTime end , decimal unit)
        {

            TimeSpan startSP = new TimeSpan(start.Ticks);
            TimeSpan endSP = new TimeSpan(end.Ticks);

            int minutes = endSP.Subtract(startSP).Minutes;
            decimal halfHour = Math.Floor(((decimal)(minutes / 30)));
            return (halfHour * unit) > 0 ? (halfHour * unit) : 0;

        }

        #endregion
    }
}