namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WidgetLoginstatusGetResponse : TopResponse
    {
        [XmlElement("is_login")]
        public bool IsLogin { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}

