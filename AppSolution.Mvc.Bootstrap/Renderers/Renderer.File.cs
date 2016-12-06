using System.Web.Mvc;
using System.Web.Mvc.Html;
using AppSolution.Mvc.Bootstrap.ControlModels;
using AppSolution.Mvc.Bootstrap.Controls;
using AppSolution.Mvc.Bootstrap.TypeExtensions;

namespace AppSolution.Mvc.Bootstrap.Renderers
{
    internal static partial class Renderer
    {
        public static string RenderFile(HtmlHelper html, BootstrapFileModel model)
        {
            string validationMessage = "";
            if (model.displayValidationMessage)
            {
                string validation = html.ValidationMessage(model.htmlFieldName).ToHtmlString();
                validationMessage = new BootstrapHelpText(validation, model.validationMessageStyle).ToHtmlString();
            }

            model.htmlAttributes.MergeHtmlAttributes(html.GetUnobtrusiveValidationAttributes(model.htmlFieldName, model.metadata));

            if (model.tooltipConfiguration != null) model.htmlAttributes.MergeHtmlAttributes(model.tooltipConfiguration.ToDictionary());
            if (model.tooltip != null) model.htmlAttributes.MergeHtmlAttributes(model.tooltip.ToDictionary());

            var mergedHtmlAttrs = model.htmlAttributes.FormatHtmlAttributes().AddOrReplace("type", "File");
            if (!string.IsNullOrEmpty(model.id)) mergedHtmlAttrs.AddOrReplace("id", model.id);

            return html.TextBox(model.htmlFieldName, null, mergedHtmlAttrs).ToHtmlString() + validationMessage;
        }
    }
}
