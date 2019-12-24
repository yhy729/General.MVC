using General.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework
{
    public static class HtmlHelperExtensions
    {
        public static bool OwnPermission(this IHtmlHelper helper, string message)
        {
            return true;
        }

        public static IWorkContext GetWorkContext(this IHtmlHelper helper)
        {
            return Core.EnginContext.Current.Resolve<IWorkContext>();
        }
    }
}
