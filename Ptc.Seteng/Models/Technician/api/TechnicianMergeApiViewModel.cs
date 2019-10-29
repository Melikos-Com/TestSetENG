using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    
    public class TechnicianMergeApiViewModel
    {

        public TechnicianMergeApiViewModel() { }


        /// <summary>
        /// 所選的群組
        /// </summary>
        public List<TechnicianGPResultApiViewModel> Groups { get; set; }
        
        /// <summary>
        /// 所選擇的技師
        /// </summary>
        public List<TechnicianResultApiViewModel> Accounts { get; set; } 

    }
}