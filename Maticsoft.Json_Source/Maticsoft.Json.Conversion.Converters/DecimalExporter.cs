namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class DecimalExporter : NumberExporterBase
    {
        public DecimalExporter() : base(typeof(decimal))
        {
        }

        protected override string ConvertToString(object value)
        {
            decimal num = (decimal) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

