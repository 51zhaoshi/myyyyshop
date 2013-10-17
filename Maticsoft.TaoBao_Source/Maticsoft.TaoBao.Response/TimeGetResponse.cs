namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TimeGetResponse : TopResponse
    {
        [XmlElement("time")]
        public string Time { get; set; }
    }
}

