namespace Maticsoft.TaoBao.Util
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class TopContext
    {
        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        internal void AddParameter(string name, string value)
        {
            this.parameters.Add(name, value);
        }

        internal void AddParameters(IDictionary<string, string> parameters)
        {
            if ((parameters != null) && (parameters.Count > 0))
            {
                IEnumerator<KeyValuePair<string, string>> enumerator = parameters.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, string> current = enumerator.Current;
                    KeyValuePair<string, string> pair2 = enumerator.Current;
                    this.AddParameter(current.Key, pair2.Value);
                }
            }
        }

        public string AppKey
        {
            get
            {
                return this["top_appkey"];
            }
        }

        public string this[string name]
        {
            get
            {
                string str;
                this.parameters.TryGetValue(name, out str);
                return str;
            }
        }

        public string SessionKey
        {
            get
            {
                return this["top_session"];
            }
        }

        public string Signature
        {
            get
            {
                return this["top_sign"];
            }
        }

        public long UserId
        {
            get
            {
                long result = 0L;
                string str = this["visitor_id"];
                if (!string.IsNullOrEmpty(str))
                {
                    long.TryParse(str, out result);
                }
                return result;
            }
        }

        public string UserNick
        {
            get
            {
                return this["visitor_nick"];
            }
        }
    }
}

