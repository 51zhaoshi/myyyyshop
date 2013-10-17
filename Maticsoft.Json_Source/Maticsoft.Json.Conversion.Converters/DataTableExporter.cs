namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Data;

    public sealed class DataTableExporter : ExporterBase
    {
        public DataTableExporter() : this(typeof(DataTable))
        {
        }

        public DataTableExporter(Type inputType) : base(inputType)
        {
        }

        internal static void ExportTable(ExportContext context, DataTable table, JsonWriter writer)
        {
            DataView defaultView = table.DefaultView;
            IExporter exporter = context.FindExporter(defaultView.GetType());
            if (exporter != null)
            {
                exporter.Export(context, defaultView, writer);
            }
            else
            {
                DataViewExporter.ExportView(context, defaultView, writer);
            }
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportTable(context, (DataTable) value, writer);
        }
    }
}

