namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterRoleInfoGetResponse : TopResponse
    {
        [XmlElement("role")]
        public Maticsoft.TaoBao.Domain.Role Role { get; set; }
    }
}

