namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaLoginAuthsignGetResponse : TopResponse
    {
        [XmlElement("subway_token")]
        public string SubwayToken { get; set; }
    }
}

