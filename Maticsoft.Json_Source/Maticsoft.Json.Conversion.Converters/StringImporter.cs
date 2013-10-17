namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class StringImporter : ImporterBase
    {
        public StringImporter() : base(typeof(string))
        {
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return this.ImportFromString(context, reader);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return this.ImportFromString(context, reader);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return ImporterBase.ReadReturning(reader, reader.Text);
        }
    }
}

