namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class DateTimeImporter : ImporterBase
    {
        public DateTimeImporter() : base(typeof(DateTime))
        {
        }

        protected override JsonException GetImportException(string jsonValueType)
        {
            return new JsonException(string.Format("Found {0} where expecting a JSON String in ISO 8601 time format or a JSON Number expressed in Unix time.", jsonValueType));
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            double num;
            object obj2;
            string text = reader.Text;
            try
            {
                num = Convert.ToDouble(text, CultureInfo.InvariantCulture);
            }
            catch (FormatException exception)
            {
                throw NumberError(exception, text);
            }
            catch (OverflowException exception2)
            {
                throw NumberError(exception2, text);
            }
            try
            {
                obj2 = ImporterBase.ReadReturning(reader, UnixTime.ToDateTime(num));
            }
            catch (ArgumentException exception3)
            {
                throw NumberError(exception3, text);
            }
            return obj2;
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            object obj2;
            try
            {
                Match match = Regex.Match(reader.Text, @"\A \/ Date \( ([0-9]+) \) \/ \z", RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    DateTime time;
                    try
                    {
                        time = UnixTime.ToDateTime((double) (((double) long.Parse(match.Groups[1].Value, NumberStyles.None, CultureInfo.InvariantCulture)) / 1000.0));
                    }
                    catch (OverflowException exception)
                    {
                        throw StringError(exception);
                    }
                    return ImporterBase.ReadReturning(reader, time);
                }
                obj2 = ImporterBase.ReadReturning(reader, XmlConvert.ToDateTime(reader.Text, XmlDateTimeSerializationMode.Local));
            }
            catch (FormatException exception2)
            {
                throw StringError(exception2);
            }
            return obj2;
        }

        private static JsonException NumberError(Exception e, string text)
        {
            return new JsonException(string.Format("Error importing JSON Number {0} as System.DateTime.", text), e);
        }

        private static JsonException StringError(Exception e)
        {
            return new JsonException("Error importing JSON String as System.DateTime.", e);
        }
    }
}

