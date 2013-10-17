namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class Int16Importer : NumberImporterBase
    {
        public Int16Importer() : base(typeof(short))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToInt16(s, CultureInfo.InvariantCulture);
        }
    }
}

