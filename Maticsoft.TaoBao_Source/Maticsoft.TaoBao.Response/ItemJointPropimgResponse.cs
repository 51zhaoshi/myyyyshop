namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemJointPropimgResponse : TopResponse
    {
        [XmlElement("prop_img")]
        public Maticsoft.TaoBao.Domain.PropImg PropImg { get; set; }
    }
}

