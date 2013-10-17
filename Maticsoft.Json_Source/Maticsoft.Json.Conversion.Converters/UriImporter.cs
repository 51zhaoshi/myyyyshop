namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class UriImporter : ImporterBase
    {
        public UriImporter() : base(typeof(Uri))
        {
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return ImporterBase.ReadReturning(reader, new Uri(reader.Text));
        }
    }
}

