using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.Infrastructure;

namespace AppSolution.Mvc.Bootstrap.Controls
{
    public class WidgetBuilder<TModel> : BuilderBase<TModel, Widget>
    {
        internal WidgetBuilder(HtmlHelper<TModel> htmlHelper, Widget widget)
            : base(htmlHelper, widget)
        {
        }

        public WidgetSectionPanel BeginHeader()
        {
            return new WidgetSectionPanel(WidgetSection.Header, base.textWriter, base.element);
        }
       
        public WidgetSectionPanel BeginBody()
        {
            return new WidgetSectionPanel(WidgetSection.Body, base.textWriter, base.element);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
