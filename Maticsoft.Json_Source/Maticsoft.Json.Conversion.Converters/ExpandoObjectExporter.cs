namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public class ExpandoObjectExporter : ExporterBase
    {
        public ExpandoObjectExporter() : base(typeof(ExpandoObject))
        {
        }

        private static void ExportMembers(ExportContext context, IEnumerable<KeyValuePair<string, object>> members, JsonWriter writer)
        {
            foreach (KeyValuePair<string, object> pair in members)
            {
                writer.WriteMember(pair.Key);
                context.Export(pair.Value, writer);
            }
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteStartObject();
            ExportMembers(context, (ExpandoObject) value, writer);
            writer.WriteEndObject();
        }
    }
}

