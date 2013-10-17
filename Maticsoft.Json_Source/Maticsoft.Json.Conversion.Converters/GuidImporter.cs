namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class GuidImporter : ImporterBase
    {
        public GuidImporter() : base(typeof(Guid))
        {
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            object obj2;
            try
            {
                obj2 = ImporterBase.ReadReturning(reader, new Guid(reader.Text.Trim()));
            }
            catch (FormatException exception)
            {
                throw new JsonException(exception.Message, exception);
            }
            return obj2;
        }
    }
}

