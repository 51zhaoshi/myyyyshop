namespace Maticsoft.TaoBao
{
    using System;
    using System.Collections.Generic;

    public class TopDictionary : Dictionary<string, string>
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public TopDictionary()
        {
        }

        public TopDictionary(IDictionary<string, string> dictionary) : base(dictionary)
        {
        }

        public void Add(string key, object value)
        {
            string str;
            if (value == null)
            {
                str = null;
            }
            else if (value is string)
            {
                str = (string) value;
            }
            else if (value is DateTime?)
            {
                DateTime? nullable = value as DateTime?;
                str = nullable.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (value is int?)
            {
                int? nullable2 = value as int?;
                str = nullable2.Value.ToString();
            }
            else if (value is long?)
            {
                long? nullable3 = value as long?;
                str = nullable3.Value.ToString();
            }
            else if (value is double?)
            {
                double? nullable4 = value as double?;
                str = nullable4.Value.ToString();
            }
            else if (value is bool?)
            {
                bool? nullable5 = value as bool?;
                str = nullable5.Value.ToString().ToLower();
            }
            else
            {
                str = value.ToString();
            }
            this.Add(key, str);
        }

        public void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                base.Add(key, value);
            }
        }

        public void AddAll(IDictionary<string, string> dict)
        {
            if ((dict != null) && (dict.Count > 0))
            {
                IEnumerator<KeyValuePair<string, string>> enumerator = dict.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, string> current = enumerator.Current;
                    this.Add(current.Key, current.Value);
                }
            }
        }
    }
}

