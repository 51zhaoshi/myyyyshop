namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class AnyImporter : ImporterBase
    {
        public AnyImporter() : base(AnyType.Value)
        {
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            JsonArray array = new JsonArray();
            ((IJsonImportable) array).Import(context, reader);
            return array;
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return BooleanObject.Box(reader.ReadBoolean());
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return reader.ReadNumber();
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            JsonObject obj2 = new JsonObject();
            ((IJsonImportable) obj2).Import(context, reader);
            return obj2;
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return reader.ReadString();
        }
    }
}

