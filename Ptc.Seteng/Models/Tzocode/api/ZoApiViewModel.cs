using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class ZoApiViewModel
    {
        public ZoApiViewModel() { }

        public ZoApiViewModel(Tzocode data)
        {
            this.ZoCd = data.ZoCd;
            this.ZoName = data.ZoName;
        }
        /// <summary>
        /// 區域代號
        /// </summary>   
        public string ZoCd { get; set; }
        /// <summary>
        /// 區域名稱
        /// </summary>
        public string ZoName { get; set; }

    }
}