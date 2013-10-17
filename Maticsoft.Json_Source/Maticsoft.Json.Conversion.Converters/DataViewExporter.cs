namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Data;

    public sealed class DataViewExporter : ExporterBase
    {
        public DataViewExporter() : this(typeof(DataView))
        {
        }

        public DataViewExporter(Type inputType) : base(inputType)
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportView(context, (DataView) value, writer);
        }

        internal static void ExportView(ExportContext context, DataView view, JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteMember("columns");
            writer.WriteStartArray();
            foreach (DataColumn column in view.Table.Columns)
            {
                context.Export(column.ColumnName, writer);
            }
            writer.WriteEndArray();
            writer.WriteMember("rows");
            writer.WriteStartArray();
            foreach (DataRowView view2 in view)
            {
                context.Export(view2.Row.ItemArray, writer);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}

