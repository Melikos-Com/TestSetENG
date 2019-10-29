using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ptc.Seteng.Repository;

namespace Ptc.Seteng.Models
{
    public class TechnicianGroupResultViewModel
    {
        
        public TechnicianGroupResultViewModel() { }

        public TechnicianGroupResultViewModel(TtechnicianGroup Data,PagedList<Tzocode> Zo) 
        {

            this.CompCd = Data.CompCd;
            this.VendorCd = Data.VendorCd;
            this.GroupName = Data.GroupName;
            this.ResponsibleZo = Data.Responsible_Zo;

            string ShowZo = "";            
            var inputZo = Data.Responsible_Zo.Split(',');
            inputZo.ForEach(x =>
            {
                ShowZo += "、" + Zo.Where(y => y.ZoCd == x && y.DoCd == "").Select(z => z.ZoName).FirstOrDefault();
            });
            ShowZo = ShowZo.Substring(1);

            string ShowDo = "";
            var inputDo = Data.Responsible_Do.Split(',');
            inputDo.ForEach(x =>
            {
                ShowDo += "、" + Zo.Where(y => y.DoCd == x).Select(z => z.DoName).FirstOrDefault();
            });
            ShowDo = ShowDo.Substring(1);


            this.ResponsibleZo = ShowZo;
            this.ResponsibleDo = ShowDo;
            this.Seq = Data.Seq;
            colData = new String[]
            {
                string.Empty,
                this.GroupName,
                this.ResponsibleZo,
                this.ResponsibleDo,
                this.CompCd,
                this.VendorCd,
                this.Seq.ToString(),
                this.IsDefault.ToString(),
            };

        }
        public string[] colData { get; set; }
        /// <summary>
        /// 公司别
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 厂商别
        /// </summary>
        public string VendorCd { get; set; }
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 厂商名稱
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// 群組序号
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 是否為預設群組
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 負責區域
        /// </summary>
        public string  ResponsibleZo { get; set; }
        /// <summary>
        /// 負責課別
        /// </summary>
        public string ResponsibleDo { get; set; }

    }
}
