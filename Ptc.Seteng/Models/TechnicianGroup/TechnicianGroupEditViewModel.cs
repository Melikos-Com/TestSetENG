using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Models
{
    public class TechnicianGroupEditViewModel
    {
        public TechnicianGroupEditViewModel() { }

        public TechnicianGroupEditViewModel(TtechnicianGroup Data)
        {
            this.Seq = Data.Seq;
            //this.CompCd = Data.CompCd;
            //this.VendorCd = Data.VendorCd;
            this.GroupName = Data.GroupName;
            this.ResponsibleZo = Data.Responsible_Zo;
            this.ResponsibleDo = Data.Responsible_Do;
        }

        /// <summary>
        /// 公司代号
        /// </summary>
        [DisplayName("公司代號")]
        public string CompCd { get; set; }
        /// <summary>
        /// 厂商代号
        /// </summary>
        [DisplayName("廠商編號")]
        public string VendorCd { get; set; }
        /// <summary>
        /// 群組序号
        /// </summary>
        [DisplayName("群組序號")]
        public int Seq { get; set; }
        /// <summary>
        /// 技師群組
        /// </summary>
        [DisplayName("群組名稱")]
        public string GroupName { get; set; }
        /// <summary>
        /// 是否為預設群組
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否自動推播
        /// </summary>
        [DisplayName("自動推播")]
        public bool IsAutoPush { get; set; }
        /// <summary>
        /// 技師姓名
        /// </summary>
        [DisplayName("技師姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 账号清单
        /// </summary>
        [DisplayName("帳號清單")]
        public string[] Accounts { get; set; }
        /// <summary>
        /// 負責區域(中文)
        /// </summary>
        [DisplayName("負責區域")]
        public string ResponsibleZo { get; set; }
        /// <summary>
        /// 負責課別(中文)
        /// </summary>
        [DisplayName("負責課別")]
        public string ResponsibleDo { get; set; }
        /// <summary>
        /// dualBox for VenderZo
        /// </summary>
        public IEnumerable<SelectListItem> VenderZoDualBoxList { get; set; }
        /// <summary>
        /// dualBox for Do
        /// </summary>
        public IEnumerable<SelectListItem> DoDualBoxList { get; set; }
        /// <summary>
        /// dualBox for accounts
        /// </summary>
        public IEnumerable<SelectListItem> AccountDualBoxList { get; set; }
        /// <summary>
        /// 操作模式
        /// </summary>
        public AuthNodeType ActionType { get; set; }
        public string[] Zo { get; set; }
        public string[] Do { get; set; }

    }
}
