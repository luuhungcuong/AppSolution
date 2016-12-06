using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.ControlModels;
using AppSolution.Mvc.Bootstrap.Controls;
using AppSolution.Mvc.Bootstrap.TypeExtensions;

namespace AppSolution.Mvc.Bootstrap.Renderers
{
    internal static partial class Renderer
    {
        public static string RenderFormGroupCustom(HtmlHelper html, string input, BootstrapLabelModel labelModel)
        {
            string label = Renderer.RenderLabel(html, labelModel ?? new BootstrapLabelModel
            {
                htmlFieldName = labelModel.htmlFieldName,
                metadata = labelModel.metadata,
                htmlAttributes = new { @class = "control-label" }.ToDictionary()
            });

            bool fieldIsValid = true;
            if (labelModel != null && labelModel.htmlFieldName != null) fieldIsValid = html.ViewData.ModelState.IsValidField(labelModel.htmlFieldName);
            return new BootstrapFormGroup(input, label, FormGroupType.textboxLike, fieldIsValid).ToHtmlString();
        }
    }
}
