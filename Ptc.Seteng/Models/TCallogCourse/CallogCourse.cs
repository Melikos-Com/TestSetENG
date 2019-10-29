using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CallogCourse
    {
        public CallogCourse()
        {

        }
        public CallogCourse(TCallogCourse data)
        {
            this.Assignor = data.Assignor;
            this.Admissibility = data.Admissibility;
            this.Datetime = data.Datetime.ToString("yyyy/MM/dd HH:mm:ss");
        }
        //公司別
        public string CompCd { get; set; }
        //案件編號
        public string Sn { get; set; }
        //改/指派者
        public string Assignor { get; set; }
        //受理者
        public string Admissibility { get; set; }
        //改/指派時間
        public string Datetime { get; set; }
    }
}