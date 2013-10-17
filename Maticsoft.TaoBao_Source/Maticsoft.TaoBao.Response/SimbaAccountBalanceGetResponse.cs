namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAccountBalanceGetResponse : TopResponse
    {
        [XmlElement("balance")]
        public string Balance { get; set; }
    }
}

