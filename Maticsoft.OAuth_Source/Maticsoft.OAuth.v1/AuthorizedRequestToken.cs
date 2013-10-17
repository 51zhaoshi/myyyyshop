namespace Maticsoft.OAuth.v1
{
    using System;

    public class AuthorizedRequestToken
    {
        private OAuthToken requestToken;
        private string verifier;

        public AuthorizedRequestToken(OAuthToken requestToken, string verifier)
        {
            this.requestToken = requestToken;
            this.verifier = verifier;
        }

        public string Secret
        {
            get
            {
                return this.requestToken.Secret;
            }
        }

        public string Value
        {
            get
            {
                return this.requestToken.Value;
            }
        }

        public string Verifier
        {
            get
            {
                return this.verifier;
            }
        }
    }
}

