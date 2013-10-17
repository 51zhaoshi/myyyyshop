namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Data;

    public sealed class DataRowExporter : ExporterBase
    {
        public DataRowExporter() : this(typeof(DataRow))
        {
        }

        public DataRowExporter(Type inputType) : base(inputType)
        {
        }

        internal static void ExportRow(ExportContext context, DataRow row, JsonWriter writer)
        {
            writer.WriteStartObject();
            foreach (DataColumn column in row.Table.Columns)
            {
                writer.WriteMember(column.ColumnName);
                context.Export(row[column], writer);
            }
            writer.WriteEndObject();
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportRow(context, (DataRow) value, writer);
        }
    }
}

