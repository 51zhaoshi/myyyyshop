namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class JsonBufferImporter : IImporter
    {
        public virtual object Import(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            return JsonBuffer.From(reader);
        }

        public Type OutputType
        {
            get
            {
                return typeof(JsonBuffer);
            }
        }
    }
}

