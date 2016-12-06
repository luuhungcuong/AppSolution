﻿using System;
using System.ComponentModel;
using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.ControlInterfaces;
using AppSolution.Mvc.Bootstrap.Infrastructure.Enums;
using AppSolution.Mvc.Bootstrap.Renderers;

namespace AppSolution.Mvc.Bootstrap.Controls
{
    public class BootstrapFormGroupFile : BootstrapFile
    {
        public BootstrapFormGroupFile(HtmlHelper html, string htmlFieldName, ModelMetadata metadata)
            : base(html, htmlFieldName, metadata)
        {
            this._model.displayValidationMessage = true;
        }

        public override IBootstrapLabel Label()
        {
            IBootstrapLabel l = new BootstrapFormGroupLabeled(html, _model, BootstrapInputType.File);
            return l;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToHtmlString()
        {
            return Renderer.RenderFormGroupFile(html, _model, null);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return ToHtmlString(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
    }
}
