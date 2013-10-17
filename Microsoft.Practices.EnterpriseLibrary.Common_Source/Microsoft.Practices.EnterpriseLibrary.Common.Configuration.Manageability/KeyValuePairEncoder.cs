namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Text;

    public class KeyValuePairEncoder
    {
        private StringBuilder builder = new StringBuilder();

        public void AppendKeyValuePair(string key, string value)
        {
            this.builder.Append(EncodeKeyValuePair(key, value, true));
            this.builder.Append(';');
        }

        public static string EncodeKeyValuePair(string key, string value)
        {
            return EncodeKeyValuePair(key, value, false);
        }

        public static string EncodeKeyValuePair(string key, string value, bool escapeSemicolons)
        {
            return (key + "=" + (escapeSemicolons ? value.Replace(";", ";;") : value));
        }

        public string GetEncodedKeyValuePairs()
        {
            return this.builder.ToString();
        }
    }
}

