namespace Maticsoft.OAuth.v1
{
    using System;

    [Serializable]
    public class OAuthToken
    {
        private string secret;
        private string value;

        public OAuthToken(string value, string secret)
        {
            this.value = value;
            this.secret = secret;
        }

        public string Secret
        {
            get
            {
                return this.secret;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

