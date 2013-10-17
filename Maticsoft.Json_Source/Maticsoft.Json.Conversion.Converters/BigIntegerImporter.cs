namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;
    using System.Numerics;

    public sealed class BigIntegerImporter : NumberImporterBase
    {
        public BigIntegerImporter() : base(typeof(BigInteger))
        {
        }

        protected override object ConvertFromString(string s)
        {
            return BigInteger.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}

