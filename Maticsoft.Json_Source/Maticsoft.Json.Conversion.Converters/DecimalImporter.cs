namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class DecimalImporter : NumberImporterBase
    {
        public DecimalImporter() : base(typeof(decimal))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return decimal.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowTrailingSign, CultureInfo.InvariantCulture);
        }
    }
}

