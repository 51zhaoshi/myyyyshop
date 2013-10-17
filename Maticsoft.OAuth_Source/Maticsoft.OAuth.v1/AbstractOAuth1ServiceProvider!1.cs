namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth;
    using System;

    public abstract class AbstractOAuth1ServiceProvider<T> : IOAuth1ServiceProvider<T>, IServiceProvider<T> where T: IApiBinding
    {
        private string consumerKey;
        private string consumerSecret;
        private IOAuth1Operations oauth1Operations;

        public AbstractOAuth1ServiceProvider(string consumerKey, string consumerSecret, IOAuth1Operations oauth1Operations)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.oauth1Operations = oauth1Operations;
        }

        public abstract T GetApi(string accessToken, string secret);

        protected string ConsumerKey
        {
            get
            {
                return this.consumerKey;
            }
        }

        protected string ConsumerSecret
        {
            get
            {
                return this.consumerSecret;
            }
        }

        public IOAuth1Operations OAuthOperations
        {
            get
            {
                return this.oauth1Operations;
            }
        }
    }
}

