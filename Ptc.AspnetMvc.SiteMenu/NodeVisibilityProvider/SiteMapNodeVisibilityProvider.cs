using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspNetMvc.Menu.SiteMap
{
    public class SiteMapNodeVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            // Is a visibility attribute specified?
            if (node.Attributes.ContainsKey("visibility") == false)
                return true;


            string visibility = (string)node.Attributes["visibility"];
            if (string.IsNullOrEmpty(visibility))
            {
                return true;
            }
            visibility = visibility.Trim();

            //process visibility
            switch (visibility)
            {
                case "tail":
                    //...
                    return false;
                default:
                    return true;
            }
        }

    }
}
