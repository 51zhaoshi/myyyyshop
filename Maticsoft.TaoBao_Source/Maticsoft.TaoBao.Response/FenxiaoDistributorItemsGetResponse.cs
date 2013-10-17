namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoDistributorItemsGetResponse : TopResponse
    {
        [XmlArray("records"), XmlArrayItem("fenxiao_item_record")]
        public List<FenxiaoItemRecord> Records { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

