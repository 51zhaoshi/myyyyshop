namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class BrandCatControl : TopObject
    {
        [XmlElement("brand_id")]
        public long BrandId { get; set; }

        [XmlElement("brand_name")]
        public string BrandName { get; set; }

        [XmlElement("cat_id")]
        public long CatId { get; set; }

        [XmlElement("cat_name")]
        public string CatName { get; set; }

        [XmlElement("certified_data")]
        public string CertifiedData { get; set; }
    }
}

