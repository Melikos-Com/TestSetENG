using MvcSiteMapProvider;
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ptc.AspNetMvc.Menu.SiteMap
{
    public class StoreDetailsDynamicNodeProvider
        : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            List<DynamicNode> retColl = new List<DynamicNode>();

            System.Security.Principal.IPrincipal principal = System.Web.HttpContext.Current.User;

            if (principal.Identity.IsAuthenticated == false)
                return retColl;

            // filter FullMenu 
            IIdentityAuth identity = (IIdentityAuth)System.Web.HttpContext.Current.User.Identity;

            //轉成sitemapNode
            foreach (var item in SiteMenu.FullMenuNode)
            {
                var newItemColl = RecursiveChild(item, null, identity.AuthItemColl);
                // var newItemColl = RecursiveChild(item, null, null);

                if (newItemColl.Count >= 1)
                    retColl.AddRange(newItemColl);
            }

            return retColl;
        }

        #region ToList

        private List<DynamicNode> RecursiveChild(
            MenuPrefixNode orgPrefixNode,
            string parentKey,
            Dictionary<string, AuthItem> authItemColl)
        {
            //準備回傳的
            List<DynamicNode> retColl = new List<DynamicNode>();

            if (orgPrefixNode.MenuNodeCollection == null)
                orgPrefixNode.MenuNodeCollection = new List<MenuControllerNode>();

            var parentDynamicNodeColl = new List<DynamicNode>();

            foreach (MenuControllerNode groupController in orgPrefixNode.MenuNodeCollection)
            {
                //使用者有權限
                if (authItemColl.ContainsKey(groupController.GroupName))
                {
                    var childDynamicNodeColl = new List<DynamicNode>();

                    //細項
                    foreach (var actionNode in groupController.subNode)
                        childDynamicNodeColl.Add(ToNode(actionNode, groupController.Name, true));

                    parentDynamicNodeColl.Add(ToNode(groupController, orgPrefixNode.Name, false));

                    //有明細才要加入parentNode
                    if (childDynamicNodeColl.Count >= 1)
                        parentDynamicNodeColl.AddRange(childDynamicNodeColl);


                }
            }

            if (parentDynamicNodeColl.Count >= 1)
            {
                retColl.AddRange(parentDynamicNodeColl);
            }



            if (orgPrefixNode.Child != null)
            {
                foreach (MenuPrefixNode childItem in orgPrefixNode.Child)
                {
                    var childNodeColl = RecursiveChild(childItem, orgPrefixNode.Name, authItemColl);
                    if (childNodeColl.Count >= 1)
                        retColl.AddRange(childNodeColl);
                }
            }

            if (retColl.Count >= 1)
                retColl.Add(ToNode(orgPrefixNode, parentKey));


            return retColl;
        }

        private DynamicNode ToNode(MenuPrefixNode prefix, string parentKey)
        {
            var ret = new DynamicNode(prefix.Name, parentKey, prefix.Name, prefix.Description);
            ret.Clickable = false;
            ret.Attributes.Add("IconCss", prefix.IconCss ?? "icon-double-angle-right");
            return ret;
        }

        private DynamicNode ToNode(MenuControllerNode menuNode, string parentKey, bool isTail, string icon = "icon-double-angle-right")
        {
            var ret = new DynamicNode(
                menuNode.Name,
                parentKey,
                menuNode.TitleName,
                menuNode.Description);

            ret.Clickable = true;
            ret.Action = menuNode.ActionName;
            ret.Controller = menuNode.ControllerName;
            ret.Attributes.Add("IconCss", icon);

            if (isTail)
                ret.Attributes.Add("isTail", "true");

            return ret;
        }

        #endregion ToList


    }
}