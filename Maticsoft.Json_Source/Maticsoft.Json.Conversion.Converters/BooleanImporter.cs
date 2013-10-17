namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class BooleanImporter : ImporterBase
    {
        public BooleanImporter() : base(typeof(bool))
        {
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return BooleanObject.Box(reader.ReadBoolean());
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            object obj2;
            try
            {
                obj2 = BooleanObject.Box(reader.ReadNumber().ToInt64() != 0L);
            }
            catch (FormatException exception)
            {
                throw new JsonException(string.Format("The JSON Number {0} must be an integer to be convertible to System.Boolean.", reader.Text), exception);
            }
            return obj2;
        }
    }
}

