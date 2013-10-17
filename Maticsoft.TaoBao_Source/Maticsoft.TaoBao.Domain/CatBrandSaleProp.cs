namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CatBrandSaleProp : TopObject
    {
        [XmlElement("brand_id")]
        public long BrandId { get; set; }

        [XmlElement("cat_id")]
        public long CatId { get; set; }

        [XmlElement("is_not_spec")]
        public bool IsNotSpec { get; set; }

        [XmlElement("property_id")]
        public long PropertyId { get; set; }
    }
}

