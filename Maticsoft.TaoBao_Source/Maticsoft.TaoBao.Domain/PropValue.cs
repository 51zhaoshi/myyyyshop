namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PropValue : TopObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("is_parent")]
        public bool IsParent { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("modified_type")]
        public string ModifiedType { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("name_alias")]
        public string NameAlias { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlElement("prop_name")]
        public string PropName { get; set; }

        [XmlElement("sort_order")]
        public long SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("vid")]
        public long Vid { get; set; }
    }
}

