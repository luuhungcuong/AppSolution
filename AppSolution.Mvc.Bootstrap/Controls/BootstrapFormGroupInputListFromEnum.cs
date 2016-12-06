using System.ComponentModel;
using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.ControlInterfaces;
using AppSolution.Mvc.Bootstrap.Infrastructure.Enums;
using AppSolution.Mvc.Bootstrap.Renderers;

namespace AppSolution.Mvc.Bootstrap.Controls
{
    public class BootstrapFormGroupInputListFromEnum : BootstrapInputListFromEnum
    {
        public BootstrapFormGroupInputListFromEnum(HtmlHelper html, string htmlFieldName, ModelMetadata metadata, BootstrapInputType inputType)
            : base(html, htmlFieldName, metadata, inputType)
        {
            this._model.displayValidationMessage = true;
        }

        public override IBootstrapLabel Label()
        {
            IBootstrapLabel l = new BootstrapFormGroupInputListFromEnumLabeled(html, _model);
            return l;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToHtmlString()
        {
            return Renderer.RenderFormGroupInputListFromEnum(html, _model);
        }
    }
}
