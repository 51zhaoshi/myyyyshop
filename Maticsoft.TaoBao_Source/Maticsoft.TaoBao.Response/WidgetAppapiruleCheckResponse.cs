namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WidgetAppapiruleCheckResponse : TopResponse
    {
        [XmlElement("call_permission")]
        public string CallPermission { get; set; }

        [XmlElement("http_method")]
        public string HttpMethod { get; set; }

        [XmlElement("need_auth")]
        public string NeedAuth { get; set; }
    }
}

