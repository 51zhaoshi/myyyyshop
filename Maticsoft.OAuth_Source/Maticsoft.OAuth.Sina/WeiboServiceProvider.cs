namespace Maticsoft.OAuth.Sina
{
    using Maticsoft.OAuth.v2;
    using System;

    public class WeiboServiceProvider : AbstractOAuth2ServiceProvider<IWeibo>
    {
        public static string AuthorizeUrl = "https://api.weibo.com/oauth2/authorize";
        public static string TokenUrl = "https://api.weibo.com/oauth2/access_token";

        public WeiboServiceProvider(string clientId, string clientSecret) : base(new WeiboOAuth2Template(clientId, clientSecret, AuthorizeUrl, TokenUrl))
        {
            if (string.IsNullOrWhiteSpace(AuthorizeUrl))
            {
                throw new ArgumentNullException("AuthorizeUrl");
            }
            if (string.IsNullOrWhiteSpace(TokenUrl))
            {
                throw new ArgumentNullException("TokenUrl");
            }
        }

        public override IWeibo GetApi(AccessGrant accessGrant)
        {
            return new WeiboTemplate(accessGrant);
        }
    }
}

