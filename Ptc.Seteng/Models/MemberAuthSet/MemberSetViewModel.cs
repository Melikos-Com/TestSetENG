using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Models
{
    public class MemberSetViewModel
    {
        #region Constructor

        public MemberSetViewModel()
        {

        }

        public MemberSetViewModel(RoleAuth role)
        {

            this.PageAuth = new List<TreeViewNode>();

            //目前已設定權限
            var currentAuth = role.PageAuth.ToDictionary(x => x.GroupName);


            //轉成sitemapNode
            foreach (var item in SiteMenu.FullMenuNode)
            {
                var newItemColl = RecursiveChild(item, null, currentAuth);

                if (newItemColl.Count >= 1)
                    this.PageAuth.AddRange(newItemColl);
            }

        }

        #endregion

        #region Property

        /// <summary>
        /// 使用者ID
        /// </summary>
        [DisplayName("使用者ID")]
        [DisplayFormat(NullDisplayText = "使用者ID")]
        public string UserID { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        [DisplayName("使用者帳號")]
        [DisplayFormat(NullDisplayText = "使用者帳號")]
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 使用者密碼
        /// </summary>
        [DisplayName("使用者密碼")]
        [DisplayFormat(NullDisplayText = "使用者密碼")]
        [Required]
        public string PasswordHash { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [DisplayName("使用者名稱")]
        [DisplayFormat(NullDisplayText = "使用者名稱")]
        [Required]
        public string CorpUserName { get; set; }
        /// <summary>
        /// 使用者電話
        /// </summary>
        [DisplayName("使用者電話")]
        [DisplayFormat(NullDisplayText = "使用者電話")]
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        [DisplayFormat(NullDisplayText = "Email")]
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// 角色權限
        /// </summary>
        [DisplayName("角色權限")]
        [DisplayFormat(NullDisplayText = "角色權限")]
        [Required]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色權限編號
        /// </summary>
        [DisplayName("角色權限編號")]
        [DisplayFormat(NullDisplayText = "角色權限編號")]
        [Required]
        public string RoleId { get; set; }
        /// <summary>
        /// 公司名稱            
        /// </summary>
        [DisplayName("公司名稱")]
        [DisplayFormat(NullDisplayText = "公司名稱")]
        [Required]
        public string Compcd { get; set; }


        /// <summary>
        /// 頁面權限
        /// </summary>
        [DisplayName("頁面權限")]
        [DisplayFormat(NullDisplayText = "頁面權限")]
        public List<TreeViewNode> PageAuth { get; set; }
      

        #endregion

        #region Extend

        /// <summary>
        /// 操作模式
        /// </summary>
        [DisplayName("操作模式")]
        [DisplayFormat(NullDisplayText = "操作模式")]
        public AuthNodeType AuthNodeType { get; set; }

        /// <summary>
        /// 角色名稱下拉選單
        /// </summary>
        [DisplayName("角色名稱下拉選單")]
        [DisplayFormat(NullDisplayText = "角色名稱下拉選單")]
        public IEnumerable<SelectListItem> RoleNameList { get; set; }

        #endregion

        #region feature

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


                    if (root.nodes == null)
                        root.nodes = childNodeColl;
                    else
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