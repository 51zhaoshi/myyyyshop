namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SellerCat : TopObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_cid")]
        public long ParentCid { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("sort_order")]
        public long SortOrder { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

