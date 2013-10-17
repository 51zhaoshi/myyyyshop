namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemImgDeleteResponse : TopResponse
    {
        [XmlElement("item_img")]
        public Maticsoft.TaoBao.Domain.ItemImg ItemImg { get; set; }
    }
}

