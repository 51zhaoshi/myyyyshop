namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FeedbackAddResponse : TopResponse
    {
        [XmlElement("result")]
        public string Result { get; set; }
    }
}

