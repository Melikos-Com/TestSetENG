using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Menu
{
    public class MenuControllerNode : MenuNode
    {

        public MenuControllerNode()
        {
            this.subNode = new List<MenuControllerNode>();
        }

        public bool BindFlag { get; set; }

        /// <summary>
        /// Controller Name
        /// </summary>
        public String ControllerName { get; set; }

        /// <summary>
        /// Action Name
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 是否為Title
        /// </summary>
        public bool isEntry { get; set; }

        public string GroupName { get; set; }

        public string PrefixedNodeID { get; set; }

        public List<MenuControllerNode> subNode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Group:[{1}]", base.Name, GroupName);

        }
    }
}
