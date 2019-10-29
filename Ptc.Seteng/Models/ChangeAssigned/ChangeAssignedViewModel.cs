using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class ChangeAssignedViewModel
    {
        public ChangeAssignedViewModel()
        { }
        /// <summary>
        /// 案件列表
        /// </summary>
        public ChangeAssignedViewModel(MobileCallogSearch Callog, string ZoName, string DoName)
        {
            this.Sn = Callog.Sn;
            this.Zo_Name = ZoName;
            this.Do_Name = DoName;
            this.Store_Name = Callog.StoreName;
            this.Asset_Name = Callog.AssetName;
            this.Damage_Desc = Callog.DamageDesc;
            this.Fi_Date = Callog.FiDate;
            this.Fd_Date = Callog.FdDate;
            this.Name = Callog.Acceptname;

            colData = new string[]
            {
                this.Sn,
                this.Sn,
                this.Zo_Name,
                this.Do_Name,
                this.Store_Name ,
                this.Asset_Name ,
                this.Damage_Desc ,
                this.Fi_Date.ToString("yyyy-MM-dd HH:mm") ,
                this.Fd_Date,
                this.Name
            };
        }
        /// <summary>
        /// 技師列表
        /// </summary>
        public ChangeAssignedViewModel(TvenderTechnician data)
        {
            this.Name = data.Name;
            this.Account = data.Account;
            colData = new string[]
            {
                string.Empty,
                this.Name,
                this.Account
            };
        }


        public String[] colData { get; set; }
        /// <summary>
        /// 叫修編號
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 區名稱
        /// </summary>
        public string Zo_Name { get; set; }
        /// <summary>
        /// 課名稱
        /// </summary>
        public string Do_Name { get; set; }
        /// <summary>
        /// 店名
        /// </summary>
        public string Store_Name { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        public string Asset_Name { get; set; }

        /// <summary>
        /// 故障原因
        /// </summary>
        public string Damage_Desc { get; set; }

        /// <summary>
        /// 立案時間
        /// </summary>
        public DateTime Fi_Date { get; set; }

        /// <summary>
        /// 應完成時間
        /// </summary>
        public string Fd_Date { get; set; }
        /// <summary>
        /// 技師姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 技師帳號
        /// </summary>
        public string Account { get; set; }
    }
}