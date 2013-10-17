namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SearchOrderResult : TopObject
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlArray("order_ids"), XmlArrayItem("number")]
        public List<long> OrderIds { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }
    }
}

