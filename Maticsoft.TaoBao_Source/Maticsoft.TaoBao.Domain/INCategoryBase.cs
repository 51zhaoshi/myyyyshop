namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INCategoryBase : TopObject
    {
        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        [XmlElement("category_pv")]
        public long CategoryPv { get; set; }

        [XmlArrayItem("i_n_record_base"), XmlArray("in_record_base_list")]
        public List<INRecordBase> InRecordBaseList { get; set; }
    }
}

