using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class LogfileApiViewModel
    {
        public LogfileApiViewModel() { }

       
        /// <summary>
        /// 傳入的檔案
        /// </summary>
        public HttpPostedFileBase[] Files { get; set; }

        public string files { get; set; }
    }
}