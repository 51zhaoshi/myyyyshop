namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public sealed class ByteImporter : NumberImporterBase
    {
        public ByteImporter() : base(typeof(byte))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return Convert.ToByte(s, CultureInfo.InvariantCulture);
        }
    }
}

