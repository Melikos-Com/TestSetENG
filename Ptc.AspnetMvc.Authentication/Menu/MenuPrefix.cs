using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Menu
{
    public class MenuPrefixNode : MenuNode
    {

        /// <summary>
        /// 上一層關係
        /// </summary>
        public String ParentName { get; set; }    

        public List<MenuPrefixNode> Child { get; set; }

        /// <summary>
        /// 此層底的的Menu Node (Title)
        /// </summary>
        public List<string> MenuNodeNameCollection { get; set; }

        /// <summary>
        /// 此層底的的MenuNode attribute都會轉成MenuNode
        /// </summary>
        public List<MenuControllerNode> MenuNodeCollection { get; set; }

        public string IconCss { get; set; }

        #region toTree
        public MenuPrefixNode ToTree(List<MenuPrefixNode> checkItemColl, List<MenuControllerNode> nodeColl)
        {
            this.Child = RecursiveChildToTree(checkItemColl, ref nodeColl, this.Name);
            return this;
        }

        private List<MenuPrefixNode> RecursiveChildToTree(List<MenuPrefixNode> PrefixColl,ref  List<MenuControllerNode> nodeColl, string Name)
        {
            //找出子Child
            List<MenuPrefixNode> childItemColl = PrefixColl.Where(x => x.ParentName == Name).ToList();

            if (this.MenuNodeCollection == null)
            {
                this.MenuNodeCollection = new List<MenuControllerNode>();
            }


            List<MenuControllerNode> controllNode = nodeColl.Where(x => x.PrefixedNodeID == this.Id).ToList();
            foreach (var node in controllNode)
            {
                node.BindFlag = true;
                this.MenuNodeCollection.Add(node);
            }



            //foreach (string item in this.MenuNodeNameCollection)
            //{
            //    //找出在這個Prefix下的Menu Node
            //    List<MenuControllerNode> controllNode = nodeColl.Where(x => x.GroupName == item).ToList();
            //    foreach (var node in controllNode)
            //    {
            //        node.BindFlag = true;
            //        this.MenuNodeCollection.Add(node);
            //    }
            //    //if (controllNode.Count >= 1)
            //    //    this.MenuNodeCollection.AddRange(controllNode);
            //}

            if (childItemColl.Count == 0)
            {
                return null;
            }

            foreach (var item in childItemColl)
            {
                item.Child = item.RecursiveChildToTree(PrefixColl, ref  nodeColl, item.Name);
            }

            //將未定義之功能顯示在ROOT節點上
            if (this.MenuNodeCollection.Count > 0)
            {
                List<string> tmp = this.MenuNodeCollection.Select(x => x.ControllerName).ToList();
                List<MenuControllerNode> nouse = nodeColl.Where(x => !x.BindFlag && tmp.Contains(x.ControllerName)).ToList();

                if (nouse.Count() > 0)
                { this.MenuNodeCollection.AddRange(nouse); }
            }

            return childItemColl;
        }
        #endregion

        public override string ToString()
        {
            return base.Name;

            //return base.ToString();
        }
    }
}
