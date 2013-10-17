namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public abstract class NumberExporterBase : ExporterBase
    {
        protected NumberExporterBase(Type inputType) : base(inputType)
        {
        }

        protected abstract string ConvertToString(object value);
        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            string str;
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            try
            {
                str = this.ConvertToString(value);
            }
            catch (InvalidCastException exception)
            {
                throw new JsonException(exception.Message, exception);
            }
            writer.WriteNumber(str);
        }
    }
}

