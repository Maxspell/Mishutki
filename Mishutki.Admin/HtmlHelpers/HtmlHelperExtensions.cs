using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mishutki.Admin.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActiveTab(this HtmlHelper helper, string activeController, string[] activeActions, string cssClass)
        {
            string currentAction = helper.ViewContext.Controller.
                    ValueProvider.GetValue("action").RawValue.ToString();
            string currentController = helper.ViewContext.Controller.
                    ValueProvider.GetValue("controller").RawValue.ToString();

            string cssClassToUse = currentController == activeController
                 && activeActions.Contains(currentAction)
                                       ? cssClass
                                       : string.Empty;
            return cssClassToUse;
        }
    }
}