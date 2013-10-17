namespace Maticsoft.OAuth.Tencent.QQ
{
    using Maticsoft.OAuth.v2;
    using System;

    public class QConnectServiceProvider : AbstractOAuth2ServiceProvider<IQConnect>
    {
        protected string _clientId;
        public static string AuthorizeUrl = "https://graph.qq.com/oauth2.0/authorize";
        public static string TokenUrl = "https://graph.qq.com/oauth2.0/token";

        public QConnectServiceProvider(string clientId, string clientSecret) : base(new QConnectOAuth2Template(clientId, clientSecret, AuthorizeUrl, TokenUrl))
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

        public override IQConnect GetApi(AccessGrant accessGrant)
        {
            return new QConnectTemplate(accessGrant, this._clientId);
        }
    }
}

