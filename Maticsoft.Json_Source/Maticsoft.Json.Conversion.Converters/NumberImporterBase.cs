namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public abstract class NumberImporterBase : ImporterBase
    {
        protected NumberImporterBase(Type type) : base(type)
        {
        }

        protected abstract object ConvertFromString(string s);
        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return Convert.ChangeType(BooleanObject.Box(reader.ReadBoolean()), base.OutputType);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            object obj2;
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            string text = reader.Text;
            try
            {
                obj2 = ImporterBase.ReadReturning(reader, this.ConvertFromString(text));
            }
            catch (FormatException exception)
            {
                throw this.NumberError(exception, text);
            }
            catch (OverflowException exception2)
            {
                throw this.NumberError(exception2, text);
            }
            return obj2;
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return this.ImportFromNumber(context, reader);
        }

        private Exception NumberError(Exception e, string text)
        {
            return new JsonException(string.Format("Error importing JSON Number {0} as {1}.", text, base.OutputType.FullName), e);
        }
    }
}

