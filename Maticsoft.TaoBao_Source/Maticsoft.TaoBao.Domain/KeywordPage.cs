namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class KeywordPage : TopObject
    {
        [XmlArrayItem("keyword"), XmlArray("keyword_list")]
        public List<Keyword> KeywordList { get; set; }

        [XmlElement("page_no")]
        public long PageNo { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("total_item")]
        public long TotalItem { get; set; }
    }
}

