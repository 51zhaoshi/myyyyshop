namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ItemProp : TopObject
    {
        [XmlElement("child_template")]
        public string ChildTemplate { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("is_allow_alias")]
        public bool IsAllowAlias { get; set; }

        [XmlElement("is_color_prop")]
        public bool IsColorProp { get; set; }

        [XmlElement("is_enum_prop")]
        public bool IsEnumProp { get; set; }

        [XmlElement("is_input_prop")]
        public bool IsInputProp { get; set; }

        [XmlElement("is_item_prop")]
        public bool IsItemProp { get; set; }

        [XmlElement("is_key_prop")]
        public bool IsKeyProp { get; set; }

        [XmlElement("is_sale_prop")]
        public bool IsSaleProp { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("modified_type")]
        public string ModifiedType { get; set; }

        [XmlElement("multi")]
        public bool Multi { get; set; }

        [XmlElement("must")]
        public bool Must { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_pid")]
        public long ParentPid { get; set; }

        [XmlElement("parent_vid")]
        public long ParentVid { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlArrayItem("prop_value"), XmlArray("prop_values")]
        public List<PropValue> PropValues { get; set; }

        [XmlElement("required")]
        public bool Required { get; set; }

        [XmlElement("sort_order")]
        public long SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

