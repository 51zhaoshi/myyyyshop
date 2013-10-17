namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Data;

    public sealed class DataRowViewExporter : ExporterBase
    {
        public DataRowViewExporter() : this(typeof(DataRowView))
        {
        }

        public DataRowViewExporter(Type inputType) : base(inputType)
        {
        }

        private static void ExportRowView(ExportContext context, DataRowView rowView, JsonWriter writer)
        {
            writer.WriteStartObject();
            foreach (DataColumn column in rowView.DataView.Table.Columns)
            {
                writer.WriteMember(column.ColumnName);
                context.Export(rowView[column.Ordinal], writer);
            }
            writer.WriteEndObject();
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportRowView(context, (DataRowView) value, writer);
        }
    }
}

