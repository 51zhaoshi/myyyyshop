namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ItemTemplate : TopObject
    {
        [XmlElement("shop_type")]
        public long ShopType { get; set; }

        [XmlElement("template_id")]
        public long TemplateId { get; set; }

        [XmlElement("template_name")]
        public string TemplateName { get; set; }
    }
}

