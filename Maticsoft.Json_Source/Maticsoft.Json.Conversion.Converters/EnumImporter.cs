namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Json.Diagnostics;
    using System;

    public sealed class EnumImporter : ImporterBase
    {
        public EnumImporter(Type type) : base(type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("{0} does not inherit from System.Enum.", type), "type");
            }
            if (type.IsDefined(typeof(FlagsAttribute), true))
            {
                throw new ArgumentException(string.Format("{0} is a bit field, which are not currently supported.", type), "type");
            }
        }

        private JsonException Error(string s, Exception e)
        {
            return new JsonException(string.Format("The value '{0}' cannot be imported as {1}.", DebugString.Format(s), base.OutputType.FullName), e);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            object obj2;
            string s = reader.Text.Trim();
            if (s.Length > 0)
            {
                char c = s[0];
                if ((char.IsDigit(c) || (c == '+')) || (c == '-'))
                {
                    throw this.Error(s, null);
                }
            }
            try
            {
                obj2 = ImporterBase.ReadReturning(reader, Enum.Parse(base.OutputType, s, true));
            }
            catch (ArgumentException exception)
            {
                throw this.Error(s, exception);
            }
            return obj2;
        }
    }
}

