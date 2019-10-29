using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.AspnetMvc.Authentication.Menu
{
    [AttributeUsage(AttributeTargets.Method , AllowMultiple = true, Inherited = false)]
    public class MenuNodeAttribute : Attribute
    {
        public MenuNodeAttribute()
        {

        }

        /// <summary>
        /// 依附在那個假的NODE底下
        /// </summary>
       public string PrefixedNodeID { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 指定Action Name
        /// </summary>
        public string ActionName { get; set; }

        public bool isEntry { get; set; }

        public string GroupName { get; set; }

        public AuthNodeType AuthType { get; set; }
    }


}