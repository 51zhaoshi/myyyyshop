namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class DoubleImporter : NumberImporterBase
    {
        public DoubleImporter() : base(typeof(double))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToDouble(s, CultureInfo.InvariantCulture);
        }
    }
}

