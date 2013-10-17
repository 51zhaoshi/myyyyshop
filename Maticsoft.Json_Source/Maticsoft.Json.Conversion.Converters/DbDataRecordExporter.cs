namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.ComponentModel;
    using System.Data.Common;

    public sealed class DbDataRecordExporter : ExporterBase
    {
        public DbDataRecordExporter() : this(typeof(DbDataRecord))
        {
        }

        public DbDataRecordExporter(Type inputType) : base(inputType)
        {
        }

        internal static void ExportRecord(ExportContext context, ICustomTypeDescriptor record, JsonWriter writer)
        {
            writer.WriteStartObject();
            foreach (PropertyDescriptor descriptor in record.GetProperties())
            {
                writer.WriteMember(descriptor.Name);
                context.Export(descriptor.GetValue(record), writer);
            }
            writer.WriteEndObject();
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportRecord(context, (DbDataRecord) value, writer);
        }
    }
}

