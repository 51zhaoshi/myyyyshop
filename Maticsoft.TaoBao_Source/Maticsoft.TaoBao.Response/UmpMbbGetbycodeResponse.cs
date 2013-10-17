namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpMbbGetbycodeResponse : TopResponse
    {
        [XmlElement("mbb")]
        public string Mbb { get; set; }
    }
}

