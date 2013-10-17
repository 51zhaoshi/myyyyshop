namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class Int64Importer : NumberImporterBase
    {
        public Int64Importer() : base(typeof(long))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToInt64(s, CultureInfo.InvariantCulture);
        }
    }
}

