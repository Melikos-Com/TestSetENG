
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;

using System;
using System.Collections.Generic;
using System.Linq;


namespace Ptc.Seteng.Models
{
    public class RoleAuthViewModel
    {
        public RoleAuthViewModel() { }

        public RoleAuthViewModel(RoleAuth roleAuth, List<AuthData> allDataRange)
        {
            if (roleAuth == null) { return; }

            this.RoleId = roleAuth.RoleId;
            this.RoleName = roleAuth.RoleName;
            this.Compcd = roleAuth.CompCd;
            this.PageAuth = new List<TreeViewNode>();
         

            #region 功能權限設定

            //目前已設定權限
            var currentAuth = roleAuth.PageAuth.ToDictionary(x => x.GroupName);
            //轉成sitemapNode
            foreach (var item in SiteMenu.FullMenuNode)
            {
                var newItemColl = RecursiveChild(item, null, currentAuth);

                if (newItemColl.Count >= 1)
                    this.PageAuth.AddRange(newItemColl);
            }

            #endregion

        

           
            this.UpdateUser = roleAuth.UpdateUser;
            this.UpdateTime = roleAuth.UpdateTime;
        }

        #region Property
        public AuthNodeType WorkType { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Compcd { get; set; }
        /// <summary>
        /// 角色代碼
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 頁面權限
        /// </summary>
        public List<TreeViewNode> PageAuth { get; set; }   
        /// <summary>
        /// 修改人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public Nullable<DateTime> UpdateTime { get; set; }
     
        #endregion

    
        #region menu

        private List<TreeViewNode> RecursiveChild(
         MenuPrefixNode orgPrefixNode,
         string parentKey,
         Dictionary<string, AuthItem> authItemColl)
        {
            //準備回傳的
            List<TreeViewNode> retColl = new List<TreeViewNode>();

            if (orgPrefixNode.MenuNodeCollection == null)
                orgPrefixNode.MenuNodeCollection = new List<MenuControllerNode>();


            //進入點
            var entryNodeColl = new List<TreeViewNode>();
            foreach (MenuControllerNode groupController in orgPrefixNode.MenuNodeCollection)
            {
                entryNodeColl.Add(ToNode(groupController, orgPrefixNode.Name, true, authItemColl));
            }



            TreeViewNode root = ToNode(orgPrefixNode, parentKey);

            if (entryNodeColl.Count >= 1)
            {
                root.nodes = entryNodeColl;
            }

            if (orgPrefixNode.Child != null)
            {
                foreach (MenuPrefixNode childItem in orgPrefixNode.Child)
                {
                    var childNodeColl = RecursiveChild(childItem, orgPrefixNode.Name, authItemColl);
                    if (childNodeColl.Count >= 1)
                        root.nodes.AddRange(childNodeColl);
                }
            }


            if (root.nodes != null && root.nodes.Count >= 1)
                retColl.Add(root);



            return retColl;
        }

        private TreeViewNode ToNode(MenuPrefixNode prefix, string parentKey)
        {
            var ret = new TreeViewNode(prefix.Name, prefix.Name, false, null);
            return ret;
        }

        private TreeViewNode ToNode(MenuControllerNode menuNode, string parentKey, bool isTail, Dictionary<string, AuthItem> authItemColl)
        {
            TreeViewNode ret;
            if (authItemColl.ContainsKey(menuNode.GroupName))
            {
                ret = new TreeViewNode(menuNode.GroupName, menuNode.TitleName, isTail, authItemColl[menuNode.GroupName].AuthType);
            }
            else
            {
                ret = new TreeViewNode(menuNode.GroupName, menuNode.TitleName, isTail, null);
            }


            return ret;
        }

        #endregion
    }
}