namespace Maticsoft.TaoBao.Util
{
    using Maticsoft.TaoBao;
    using System;

    public sealed class RequestValidator
    {
        private const string ERR_CODE_PARAM_INVALID = "41";
        private const string ERR_CODE_PARAM_MISSING = "40";
        private const string ERR_MSG_PARAM_INVALID = "client-error:Invalid arguments:{0}";
        private const string ERR_MSG_PARAM_MISSING = "client-error:Missing required arguments:{0}";

        public static void ValidateMaxLength(string name, FileItem value, int maxLength)
        {
            if (((value != null) && (value.GetContent() != null)) && (value.GetContent().Length > maxLength))
            {
                throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
            }
        }

        public static void ValidateMaxLength(string name, string value, int maxLength)
        {
            if ((value != null) && (value.Length > maxLength))
            {
                throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
            }
        }

        public static void ValidateMaxListSize(string name, string value, int maxSize)
        {
            if (value != null)
            {
                string[] strArray = value.Split(new char[] { ',' });
                if ((strArray != null) && (strArray.Length > maxSize))
                {
                    throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
                }
            }
        }

        public static void ValidateMaxValue(string name, long? value, long maxValue)
        {
            if (value.HasValue)
            {
                long? nullable = value;
                long num = maxValue;
                if ((nullable.GetValueOrDefault() > num) && nullable.HasValue)
                {
                    throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
                }
            }
        }

        public static void ValidateMinLength(string name, string value, int minLength)
        {
            if ((value != null) && (value.Length < minLength))
            {
                throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
            }
        }

        public static void ValidateMinValue(string name, long? value, long minValue)
        {
            if (value.HasValue)
            {
                long? nullable = value;
                long num = minValue;
                if ((nullable.GetValueOrDefault() < num) && nullable.HasValue)
                {
                    throw new TopException("41", string.Format("client-error:Invalid arguments:{0}", name));
                }
            }
        }

        public static void ValidateRequired(string name, object value)
        {
            if (value == null)
            {
                throw new TopException("40", string.Format("client-error:Missing required arguments:{0}", name));
            }
            if (value.GetType() == typeof(string))
            {
                string str = value as string;
                if (string.IsNullOrEmpty(str))
                {
                    throw new TopException("40", string.Format("client-error:Missing required arguments:{0}", name));
                }
            }
        }
    }
}

