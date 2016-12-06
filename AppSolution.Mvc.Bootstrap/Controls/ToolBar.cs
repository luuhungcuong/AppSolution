using System.Collections.Generic;
using AppSolution.Mvc.Bootstrap.Infrastructure;

namespace AppSolution.Mvc.Bootstrap
{
    public class ToolBar : HtmlElement
    {
        public ToolBar()
            : base("div")
        {
            EnsureClass("btn-toolbar");
        }

        public ToolBar HtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            SetHtmlAttributes(htmlAttributes);
            return this;
        }

        public ToolBar HtmlAttributes(object htmlAttributes)
        {
            SetHtmlAttributes(htmlAttributes);
            return this;
        }
    }
}
