namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INCategoryTop : TopObject
    {
        [XmlArrayItem("i_n_category_child_top"), XmlArray("category_child_top_list")]
        public List<INCategoryChildTop> CategoryChildTopList { get; set; }

        [XmlElement("category_desc")]
        public string CategoryDesc { get; set; }

        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        [XmlArrayItem("i_n_category_properties"), XmlArray("category_properties_list")]
        public List<INCategoryProperties> CategoryPropertiesList { get; set; }
    }
}

