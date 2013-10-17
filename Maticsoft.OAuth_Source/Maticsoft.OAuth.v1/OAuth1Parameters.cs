namespace Maticsoft.OAuth.v1
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [Serializable]
    public class OAuth1Parameters : NameValueCollection
    {
        private const string OAUTH_CALLBACK = "oauth_callback";

        public OAuth1Parameters() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        protected OAuth1Parameters(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected string GetFirst(string key)
        {
            string[] values = this.GetValues(key);
            if ((values != null) && (values.Length != 0))
            {
                return values[0];
            }
            return null;
        }

        public string CallbackUrl
        {
            get
            {
                return this.GetFirst("oauth_callback");
            }
            set
            {
                this.Set("oauth_callback", value);
            }
        }
    }
}

