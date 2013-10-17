namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Data;

    public sealed class DataSetExporter : ExporterBase
    {
        public DataSetExporter() : this(typeof(DataSet))
        {
        }

        public DataSetExporter(Type inputType) : base(inputType)
        {
        }

        private static void ExportDataSet(ExportContext context, DataSet dataSet, JsonWriter writer)
        {
            writer.WriteStartObject();
            foreach (DataTable table in dataSet.Tables)
            {
                writer.WriteMember(table.TableName);
                IExporter exporter = context.FindExporter(table.GetType());
                if (exporter != null)
                {
                    exporter.Export(context, table, writer);
                }
                else
                {
                    DataTableExporter.ExportTable(context, table, writer);
                }
            }
            writer.WriteEndObject();
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportDataSet(context, (DataSet) value, writer);
        }
    }
}

