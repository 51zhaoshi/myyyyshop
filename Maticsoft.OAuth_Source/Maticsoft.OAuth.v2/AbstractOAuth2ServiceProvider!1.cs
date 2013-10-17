namespace Maticsoft.OAuth.v2
{
    using Maticsoft.OAuth;
    using System;

    public abstract class AbstractOAuth2ServiceProvider<T> : IOAuth2ServiceProvider<T>, IServiceProvider<T> where T: IApiBinding
    {
        private IOAuth2Operations oauth2Operations;

        public AbstractOAuth2ServiceProvider(IOAuth2Operations oauth2Operations)
        {
            this.oauth2Operations = oauth2Operations;
        }

        public abstract T GetApi(AccessGrant accessGrant);

        public IOAuth2Operations OAuthOperations
        {
            get
            {
                return this.oauth2Operations;
            }
        }
    }
}

