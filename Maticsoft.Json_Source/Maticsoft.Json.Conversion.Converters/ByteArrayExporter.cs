namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class ByteArrayExporter : ExporterBase
    {
        public ByteArrayExporter() : base(typeof(byte[]))
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            byte[] inArray = (byte[]) value;
            writer.WriteString(Convert.ToBase64String(inArray));
        }
    }
}

