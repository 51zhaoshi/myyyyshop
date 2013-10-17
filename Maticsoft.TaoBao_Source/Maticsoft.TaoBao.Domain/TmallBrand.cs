namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallBrand : TopObject
    {
        [XmlElement("brand_id")]
        public long BrandId { get; set; }

        [XmlElement("brand_name")]
        public string BrandName { get; set; }
    }
}

