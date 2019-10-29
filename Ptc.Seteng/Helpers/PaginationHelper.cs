using System.Web.Mvc;
using MvcPaging;

namespace Ptc.Seteng.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString PaginationLink(this HtmlHelper helper, PaginationModel model)
        {
            var ulBuilder = new TagBuilder("ul");
            ulBuilder.MergeAttribute("class", "pagination");

            foreach (var link in model.PaginationLinks)
            {
                var liBuilder = new TagBuilder("li");
                if (link.IsCurrent)
                {
                    liBuilder.MergeAttribute("class", "active");
                }
                if (!link.Active)
                {
                    liBuilder.MergeAttribute("class", "disabled");
                }

                var aBuilder = new TagBuilder("a");
                if (link.Url == null)
                {
                    aBuilder.MergeAttribute("href", "#");
                }
                else
                {
                    aBuilder.MergeAttribute("href", link.Url);
                }

                //AJAX support
                if (model.AjaxOptions != null)
                {
                    foreach (var ajaxOpt in model.AjaxOptions.ToUnobtrusiveHtmlAttributes())
                    {
                        aBuilder.MergeAttribute(ajaxOpt.Key, ajaxOpt.Value.ToString(), true);
                    }
                }

                aBuilder.SetInnerText(link.DisplayText);

                liBuilder.InnerHtml = aBuilder.ToString();
                ulBuilder.InnerHtml += liBuilder.ToString();
            }

            return new MvcHtmlString(ulBuilder.ToString());
        }
    }
}