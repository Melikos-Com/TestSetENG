using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public enum PagingSubmitType
    {
        none,
        /// <summary>
        /// 下載EXCEL
        /// </summary>
        Excel,
        /// <summary>
        /// 下載PDF
        /// </summary>
        Pdf,
        /// <summary>
        /// PDF Preview
        /// </summary>
        Preview,
       
    }
}