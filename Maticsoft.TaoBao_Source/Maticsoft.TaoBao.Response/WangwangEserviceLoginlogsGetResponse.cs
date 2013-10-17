namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceLoginlogsGetResponse : TopResponse
    {
        [XmlElement("count")]
        public long Count { get; set; }

        [XmlArrayItem("login_log"), XmlArray("loginlogs")]
        public List<LoginLog> Loginlogs { get; set; }

        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}

