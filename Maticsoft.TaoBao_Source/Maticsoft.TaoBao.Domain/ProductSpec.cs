namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ProductSpec : TopObject
    {
        [XmlElement("barcode")]
        public string Barcode { get; set; }

        [XmlArray("certified_pics"), XmlArrayItem("cert_pic_info")]
        public List<CertPicInfo> CertifiedPics { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("spec_id")]
        public long SpecId { get; set; }

        [XmlElement("spec_props")]
        public string SpecProps { get; set; }

        [XmlElement("spec_props_alias")]
        public string SpecPropsAlias { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }
    }
}

