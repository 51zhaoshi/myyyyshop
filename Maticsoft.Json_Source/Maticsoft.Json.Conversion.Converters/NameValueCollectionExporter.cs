namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Specialized;

    public sealed class NameValueCollectionExporter : ExporterBase
    {
        public NameValueCollectionExporter() : this(typeof(NameValueCollection))
        {
        }

        public NameValueCollectionExporter(Type inputType) : base(inputType)
        {
        }

        private static void ExportCollection(ExportContext context, NameValueCollection collection, JsonWriter writer)
        {
            writer.WriteStartObject();
            for (int i = 0; i < collection.Count; i++)
            {
                writer.WriteMember(collection.GetKey(i));
                string[] values = collection.GetValues(i);
                if (values == null)
                {
                    writer.WriteNull();
                }
                else if (values.Length > 1)
                {
                    context.Export(values, writer);
                }
                else
                {
                    context.Export(values[0], writer);
                }
            }
            writer.WriteEndObject();
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ExportCollection(context, (NameValueCollection) value, writer);
        }
    }
}

