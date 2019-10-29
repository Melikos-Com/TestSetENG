using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Menu
{
    public static class SiteMenu
    {
        /// <summary>
        /// 從Controller取得的GroupNode
        /// </summary>
        /// <remarks>
        /// 可設定權限的項目
        /// </remarks>
        public static List<MenuControllerNode> EntryNode { get; set; }

        /// <summary>
        /// 全部的Node結果 PrefixNode+EntryNode+ActionNode
        /// </summary>
        public static List<MenuPrefixNode> FullMenuNode { get; set; }

        public static void Init(List<MenuPrefixNode> menuPrefixColl)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            //取得Contrller上的資訊
            EntryNode = GetGroupNode(currentDomain.GetAssemblies().Where(x => x.FullName.ToLower().Contains("ptc")).ToArray());

            //找出固定Node的第一層
            FullMenuNode = menuPrefixColl.Where(x => x.ParentName == string.Empty).ToList();

            //組合
            foreach (MenuPrefixNode prefix in FullMenuNode)
                prefix.ToTree(menuPrefixColl, EntryNode);

            return;
        }

        /// <summary>
        /// 取得Assembly 內的Menu項目
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        public static List<MenuPrefixNode> GetMenuNode(System.Reflection.Assembly[] asmColl, List<MenuPrefixNode> menuPrefixColl)
        {
            //取得Contrller上的資訊
            var entryNodeColl = GetGroupNode(asmColl);

            //找出固定Node的第一層
            var preFixNodeTopLeveColl = menuPrefixColl.Where(x => x.ParentName == string.Empty).ToList();

            //組合
            foreach (MenuPrefixNode prefix in preFixNodeTopLeveColl)
                prefix.ToTree(menuPrefixColl, entryNodeColl);

            return preFixNodeTopLeveColl;
        }

        private static List<MenuControllerNode> GetGroupNode(System.Reflection.Assembly[] asmColl)
        {
            List<MenuControllerNode> menuNodeColl = new List<MenuControllerNode>();
            List<MenuControllerNode> entryNodeColl = new List<MenuControllerNode>();
            foreach (System.Reflection.Assembly asm in asmColl)
            {
                if (asm == null)
                {
                    continue;
                }

                //取得assembly內的Type
                foreach (System.Type t in asm.GetExportedTypes())
                {
                    List<MenuControllerNode> controllerMenuNodeColl = new List<MenuControllerNode>();

                    //Base為BaseController
                    if (t.BaseType != null && t.BaseType.Name == "BaseController")
                    {
                        //每個method
                        foreach (System.Reflection.MemberInfo methodInfo in t.GetMethods())
                        {
                            //嘗試找出Attribute
                            MenuNodeAttribute[] methodAttribute = (MenuNodeAttribute[])Attribute.GetCustomAttributes(methodInfo, typeof(MenuNodeAttribute));
                            List<MenuNodeAttribute> methodAttributeColl = methodAttribute.ToList();

                            //轉換成MenuControllerNode
                            if (methodAttributeColl.Count > 0)
                            {
                                string controllerName = t.Name.Replace("Controller", "");
                                MenuNodeAttribute att = methodAttributeColl.FirstOrDefault();
                                MenuControllerNode menu = new MenuControllerNode();
                                menu.ActionName = string.IsNullOrEmpty(att.ActionName) == true ? methodInfo.Name : att.ActionName;
                                menu.ControllerName = controllerName;
                                menu.Description = att.Description;
                                menu.TitleName = att.Title;
                                menu.PrefixedNodeID = att.PrefixedNodeID;
                                menu.isEntry = att.isEntry;
                                menu.GroupName = string.IsNullOrEmpty(att.GroupName) ? controllerName : controllerName + "_" + att.GroupName;
                                menu.Name = menu.ControllerName + menu.ActionName + controllerMenuNodeColl.Where(x => x.ActionName == menu.ActionName).Count();
                                controllerMenuNodeColl.Add(menu);
                            }
                        }

                    }

                    if (controllerMenuNodeColl.Count >= 1)
                        menuNodeColl.AddRange(controllerMenuNodeColl);

                }

            }

            //取出有isEntry的
            entryNodeColl = menuNodeColl.Where(x => x.isEntry).ToList();

            //組合相同Group的Node,這樣會忽略Group設定錯誤的
            foreach (var entryNode in entryNodeColl)
                entryNode.subNode = menuNodeColl.Where(x => x.GroupName == entryNode.GroupName && x.isEntry == false).ToList();

            return entryNodeColl;
        }
    }
}
