namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class JipiaoPolicyProcessResponse : TopResponse
    {
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

        [XmlElement("policy_id")]
        public long PolicyId { get; set; }
    }
}

