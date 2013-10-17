namespace Maticsoft.OAuth.Tencent.Weibo
{
    using Maticsoft.OAuth.v2;
    using System;

    public class WeiboServiceProvider : AbstractOAuth2ServiceProvider<IWeibo>
    {
        protected string _clientId;
        public static string AuthorizeUrl = "https://open.t.qq.com/cgi-bin/oauth2/authorize";
        public static string TokenUrl = "https://open.t.qq.com/cgi-bin/oauth2/access_token";

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
            this._clientId = clientId;
        }

        public override IWeibo GetApi(AccessGrant accessGrant)
        {
            return new WeiboTemplate(accessGrant, this._clientId);
        }
    }
}

