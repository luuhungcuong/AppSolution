using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.Infrastructure.Enums;

namespace AppSolution.Mvc.Bootstrap.ControlModels
{
    public class BootstrapInputListFromEnumModel
    {
        public string htmlFieldName;
        public ModelMetadata metadata;
        public BootstrapInputType inputType;
        public int? numberOfColumns;
        public int columnPixelWidth;
        public bool displayInColumnsCondition;
        public bool displayInlineBlock;
        public int marginRightPx;

        public bool displayValidationMessage;
        public HelpTextStyle validationMessageStyle;
    }
}
