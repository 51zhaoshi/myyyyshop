namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class SingleImporter : NumberImporterBase
    {
        public SingleImporter() : base(typeof(float))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToSingle(s, CultureInfo.InvariantCulture);
        }
    }
}

