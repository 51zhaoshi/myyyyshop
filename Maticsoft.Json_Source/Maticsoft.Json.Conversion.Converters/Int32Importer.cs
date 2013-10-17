namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class Int32Importer : NumberImporterBase
    {
        public Int32Importer() : base(typeof(int))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToInt32(s, CultureInfo.InvariantCulture);
        }
    }
}

