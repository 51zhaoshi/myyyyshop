namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public sealed class ControlExporter : ExporterBase
    {
        public ControlExporter() : this(typeof(Control))
        {
        }

        public ControlExporter(Type inputType) : base(inputType)
        {
        }

        private static void ExportControl(Control control, JsonWriter writer)
        {
            StringWriter innerWriter = new StringWriter();
            HtmlTextWriter htmlWriter = GetHtmlWriter(innerWriter);
            control.RenderControl(htmlWriter);
            writer.WriteString(innerWriter.ToString());
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportControl((Control) value, writer);
        }

        private static HtmlTextWriter GetHtmlWriter(TextWriter innerWriter)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                return (HtmlTextWriter) Activator.CreateInstance(current.Request.Browser.TagWriter, new object[] { innerWriter });
            }
            return new HtmlTextWriter(innerWriter);
        }
    }
}

