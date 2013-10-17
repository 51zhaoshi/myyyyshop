namespace Maticsoft.Json
{
    using System;

    internal sealed class Mask
    {
        private Mask()
        {
            throw new NotSupportedException();
        }

        public static string EmptyString(string actual, string emptyValue)
        {
            if (NullString(actual).Length != 0)
            {
                return actual;
            }
            return emptyValue;
        }

        public static string NullString(string actual)
        {
            if (actual != null)
            {
                return actual;
            }
            return string.Empty;
        }

        public static string NullString(string actual, string mask)
        {
            if (actual != null)
            {
                return actual;
            }
            return mask;
        }
    }
}

