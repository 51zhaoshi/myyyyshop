namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ItemCat : TopObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlArrayItem("feature"), XmlArray("features")]
        public List<Feature> Features { get; set; }

        [XmlElement("is_parent")]
        public bool IsParent { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("modified_type")]
        public string ModifiedType { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_cid")]
        public long ParentCid { get; set; }

        [XmlElement("sort_order")]
        public long SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}

