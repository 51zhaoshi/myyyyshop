namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections;

    public sealed class DictionaryExporter : ExporterBase
    {
        public DictionaryExporter(Type inputType) : base(inputType)
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            writer.WriteStartObject();
            IDictionary dictionary = (IDictionary) value;
            foreach (DictionaryEntry entry in DictionaryHelper.GetEntries(dictionary))
            {
                writer.WriteMember(entry.Key.ToString());
                context.Export(entry.Value, writer);
            }
            writer.WriteEndObject();
        }
    }
}

