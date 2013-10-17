namespace Maticsoft.Json
{
    using System;
    using System.Globalization;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct JsonNumber : IConvertible
    {
        private readonly string _value;
        private static readonly System.Text.RegularExpressions.Regex[] _grammars;
        public JsonNumber(string value)
        {
            if ((value != null) && !IsValid(value))
            {
                throw new ArgumentException("value");
            }
            this._value = value;
        }

        private string Value
        {
            get
            {
                return Mask.EmptyString(this._value, "0");
            }
        }
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj is JsonNumber)) && this.Equals((JsonNumber) obj));
        }

        public bool Equals(JsonNumber other)
        {
            return this.Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return this.Value;
        }

        public bool LogicallyEquals(object o)
        {
            if (o == null)
            {
                return false;
            }
            return Convert.ChangeType(this, o.GetType(), CultureInfo.InvariantCulture).Equals(o);
        }

        public bool ToBoolean()
        {
            return (this.ToInt64() != 0L);
        }

        public char ToChar()
        {
            return (char) Convert.ToUInt16(this.Value, CultureInfo.InvariantCulture);
        }

        public byte ToByte()
        {
            return Convert.ToByte(this.Value, CultureInfo.InvariantCulture);
        }

        public short ToInt16()
        {
            return Convert.ToInt16(this.Value, CultureInfo.InvariantCulture);
        }

        public int ToInt32()
        {
            return Convert.ToInt32(this.Value, CultureInfo.InvariantCulture);
        }

        public long ToInt64()
        {
            return Convert.ToInt64(this.Value, CultureInfo.InvariantCulture);
        }

        public float ToSingle()
        {
            return Convert.ToSingle(this.Value, CultureInfo.InvariantCulture);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(this.Value, CultureInfo.InvariantCulture);
        }

        public decimal ToDecimal()
        {
            return decimal.Parse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public BigInteger ToBigInteger()
        {
            return this.ToBigInteger(CultureInfo.InvariantCulture);
        }

        private BigInteger ToBigInteger(IFormatProvider provider)
        {
            return BigInteger.Parse(this.Value, NumberStyles.Integer, provider);
        }

        public DateTime ToDateTime()
        {
            return UnixTime.ToDateTime(this.ToInt64());
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return this.ToBoolean();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return this.ToChar();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.ToInt32());
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return this.ToByte();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return this.ToInt16();
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Value, provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return this.ToInt32();
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Value, provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return this.ToInt64();
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Value, provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return this.ToSingle();
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return this.ToDouble();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return this.ToDecimal();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return this.ToDateTime();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(BigInteger))
            {
                return this.ToBigInteger(provider);
            }
            return Convert.ChangeType(this, conversionType, provider);
        }

        public static explicit operator byte(JsonNumber number)
        {
            return number.ToByte();
        }

        public static explicit operator short(JsonNumber number)
        {
            return number.ToInt16();
        }

        public static explicit operator int(JsonNumber number)
        {
            return number.ToInt32();
        }

        public static explicit operator long(JsonNumber number)
        {
            return number.ToInt64();
        }

        public static explicit operator float(JsonNumber number)
        {
            return number.ToSingle();
        }

        public static explicit operator double(JsonNumber number)
        {
            return number.ToDouble();
        }

        public static explicit operator decimal(JsonNumber number)
        {
            return number.ToDecimal();
        }

        public static explicit operator DateTime(JsonNumber number)
        {
            return number.ToDateTime();
        }

        public static bool IsValid(string text)
        {
            return IsValid(text, NumberStyles.None);
        }

        public static bool IsValid(string text, NumberStyles styles)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if ((styles < NumberStyles.None) || (styles >= _grammars.Length))
            {
                throw new ArgumentException(null, "styles");
            }
            return _grammars[(int) styles].IsMatch(text);
        }

        private static System.Text.RegularExpressions.Regex Regex(bool lws, bool rws)
        {
            return new System.Text.RegularExpressions.Regex("^" + (lws ? @"\s*" : null) + "      -?                # [ minus ]\r\n                                            # int\r\n                        (  0                #   zero\r\n                           | [1-9][0-9]* )  #   / ( digit1-9 *DIGIT )\r\n                                            # [ frac ]\r\n                        ( \\.                #   decimal-point \r\n                          [0-9]+ )?         #   1*DIGIT\r\n                                            # [ exp ]\r\n                        ( [eE]              #   e\r\n                          [+-]?             #   [ minus / plus ]\r\n                          [0-9]+ )?         #   1*DIGIT\r\n                  " + (rws ? @"\s*" : null) + "$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        }

        static JsonNumber()
        {
            _grammars = new System.Text.RegularExpressions.Regex[] { Regex(false, false), Regex(true, false), Regex(false, true), Regex(true, true) };
        }
    }
}

