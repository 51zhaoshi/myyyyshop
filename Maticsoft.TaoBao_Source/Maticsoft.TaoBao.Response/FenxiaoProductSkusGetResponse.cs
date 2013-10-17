namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductSkusGetResponse : TopResponse
    {
        [XmlArray("skus"), XmlArrayItem("fenxiao_sku")]
        public List<FenxiaoSku> Skus { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

