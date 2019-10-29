using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Menu
{
    public class MenuNode
    {

        public virtual String Id { get; set; }

        public virtual String Name { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


    }
}
