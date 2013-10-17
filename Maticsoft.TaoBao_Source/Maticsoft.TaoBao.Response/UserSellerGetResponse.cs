namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UserSellerGetResponse : TopResponse
    {
        [XmlElement("user")]
        public Maticsoft.TaoBao.Domain.User User { get; set; }
    }
}

