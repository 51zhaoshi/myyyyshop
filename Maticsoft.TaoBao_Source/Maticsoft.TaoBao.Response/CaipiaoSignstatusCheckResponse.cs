namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoSignstatusCheckResponse : TopResponse
    {
        [XmlElement("sign")]
        public bool Sign { get; set; }

        [XmlElement("sign_url")]
        public string SignUrl { get; set; }
    }
}

