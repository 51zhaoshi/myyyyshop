namespace Maticsoft.Common.DEncrypt
{
    using System;
    using System.Globalization;

    public static class Hex16
    {
        public static string Decode(string strDecode)
        {
            string str = "";
            try
            {
                for (int i = 0; i < (strDecode.Length / 4); i++)
                {
                    str = str + ((char) ((ushort) short.Parse(strDecode.Substring(i * 4, 4), NumberStyles.HexNumber)));
                }
            }
            catch
            {
            }
            return str;
        }

        public static string Encode(string strEncode)
        {
            string str = "";
            try
            {
                foreach (short num in strEncode.ToCharArray())
                {
                    str = str + num.ToString("X4");
                }
            }
            catch
            {
            }
            return str;
        }
    }
}

