namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RouteAlpPage : TopObject
    {
        [XmlArray("datas"), XmlArrayItem("complex_logistics_route")]
        public List<ComplexLogisticsRoute> Datas { get; set; }

        [XmlElement("end")]
        public long End { get; set; }

        [XmlElement("page_count")]
        public long PageCount { get; set; }

        [XmlElement("page_index")]
        public long PageIndex { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("record_count")]
        public long RecordCount { get; set; }

        [XmlElement("start")]
        public long Start { get; set; }
    }
}

