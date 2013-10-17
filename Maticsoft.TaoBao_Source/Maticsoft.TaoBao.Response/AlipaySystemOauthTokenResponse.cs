namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipaySystemOauthTokenResponse : TopResponse
    {
        [XmlElement("access_token")]
        public string AccessToken { get; set; }

        [XmlElement("alipay_user_id")]
        public string AlipayUserId { get; set; }

        [XmlElement("expires_in")]
        public string ExpiresIn { get; set; }

        [XmlElement("re_expires_in")]
        public string ReExpiresIn { get; set; }

        [XmlElement("refresh_token")]
        public string RefreshToken { get; set; }
    }
}

