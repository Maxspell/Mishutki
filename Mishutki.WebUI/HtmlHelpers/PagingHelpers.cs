using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mishutki.WebUI.Models;

namespace Mishutki.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
           PagingInfo pagingInfo,
           Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"pagination\"><ul>");
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                if (i == pagingInfo.CurrentPage)
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", pageUrl(i));
                    a.InnerHtml = i.ToString();

                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("active");
                    li.InnerHtml = (a.ToString());
                    result.Append(li.ToString());
                }
                else
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", pageUrl(i));
                    a.InnerHtml = i.ToString();

                    TagBuilder li = new TagBuilder("li");
                    li.InnerHtml = (a.ToString());
                    result.Append(li.ToString());
                }
            }
            result.Append("</ul></div>");
            return MvcHtmlString.Create(result.ToString());
        }
    }
}