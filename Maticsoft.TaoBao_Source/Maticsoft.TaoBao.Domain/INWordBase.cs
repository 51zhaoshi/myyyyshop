namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INWordBase : TopObject
    {
        [XmlArrayItem("i_n_record_base"), XmlArray("in_record_base_list")]
        public List<INRecordBase> InRecordBaseList { get; set; }

        [XmlElement("word")]
        public string Word { get; set; }
    }
}

