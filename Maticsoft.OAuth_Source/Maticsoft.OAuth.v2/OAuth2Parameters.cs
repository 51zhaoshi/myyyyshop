namespace Maticsoft.OAuth.v2
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [Serializable]
    public class OAuth2Parameters : NameValueCollection
    {
        private const string REDIRECT_URL = "redirect_uri";
        private const string SCOPE = "scope";
        private const string STATE = "state";

        public OAuth2Parameters() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        protected OAuth2Parameters(SerializationInfo info, StreamingContext context) : base(info, context)
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

        public string RedirectUrl
        {
            get
            {
                return this.GetFirst("redirect_uri");
            }
            set
            {
                this.Set("redirect_uri", value);
            }
        }

        public string Scope
        {
            get
            {
                return this.GetFirst("scope");
            }
            set
            {
                this.Set("scope", value);
            }
        }

        public string State
        {
            get
            {
                return this.GetFirst("state");
            }
            set
            {
                this.Set("state", value);
            }
        }
    }
}

